using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.DataAccess.Repository;
using Client.Desktop.Models;
using Client.Desktop.Utils;

namespace Client.Desktop.Sync
{
    public static class SyncProvider
    {
        private const int Count = 10;

        private static bool isSyncSuccess;

        private static bool isSendSuccess;

        public static async Task<bool> UpdateToken()
        {
            var expiredIn = Properties.Login.Default.ExpiresIn;
            if (DateTime.Now.AddMinutes(10) > expiredIn)
            {
                var accountId = Properties.Login.Default.AccountId;
                var refreshToken = AccountRepository.GetAccount(accountId).RefreshToken;
                var refreshResponse = await ApiAuthService.PostAsync(ApiRequestEnum.RefreshToken, new { AccountId = accountId, RefreshToken = refreshToken });
                if (refreshResponse != null && !refreshResponse.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(refreshResponse);
                    return false;
                }

                var responseJwtToken = await refreshResponse.Content.ReadAsAsync<ResponseJWTFormat>();
                Properties.Login.Default.JwtToken = responseJwtToken.AccessToken;
                Properties.Login.Default.ExpiresIn = responseJwtToken.ExpiresIn;
                AccountRepository.SetLoginAccount(accountId, responseJwtToken.RefreshToken);
                Properties.Login.Default.Save();
            }

            var response = await ApiAuthService.GetAsync(ApiRequestEnum.Health);
            if (response != null && !response.IsSuccessStatusCode)
            {
                MessageBoxExtension.ShowError(response);
                return false;
            }

            return true;
        }

        public static async Task<int> SyncStateCheck()
        {
            var syncStateResponse = await ApiAuthService.GetAsync(ApiRequestEnum.SyncState);
            if (syncStateResponse != null && !syncStateResponse.IsSuccessStatusCode)
            {
                MessageBoxExtension.ShowError(syncStateResponse);
                return -1;
            }

            var syncStateResult = syncStateResponse.Content.ReadAsAsync<SyncStateResponseParameters>().Result;
            return syncStateResult.USN;
        }

        public static async Task IncrementalSync()
        {
            var currentUsn = Properties.App.Default.LastUSN;
            do
            {
                var syncChunkResponse = await ApiAuthService.GetAsync(ApiRequestEnum.SyncChunk, new Tuple<string, string>("startAt", currentUsn.ToString()), new Tuple<string, string>("count", Count.ToString()));
                if (syncChunkResponse != null && !syncChunkResponse.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(syncChunkResponse);
                    return;
                }

                var syncStateResult = syncChunkResponse.Content.ReadAsAsync<List<SyncChunkResponseParameters>>().Result;

                if (!syncStateResult.Any()) break;

                foreach (var change in syncStateResult)
                {
                    ProcessChange(change);
                    if (isSyncSuccess)
                    {
                        currentUsn = change.USN;
                    }
                    else
                    {
                        MessageBoxExtension.ShowError($"Error in Incremental Sync. Table: {change.Table}, column: {change.Column}, value: {change.Value}, rowId: {change.RowId}, USN: {change.USN}");
                        return;
                    }
                }

            } while (true);

            Properties.App.Default.LastUSN = currentUsn;
            Properties.App.Default.Save();
        }

        public static async Task SendChanges()
        {
            await SendCashFlow();
            await SendCashAccount();
            await SendCashFlowCategory();
        }

        private static void ProcessChange(SyncChunkResponseParameters change)
        {
            switch (change.Table)
            {
                case "CashFlow":
                    ProcessCashFlow(change);
                    break;
                case "CashFlowCategory":
                    ProcessCashFlowCategory(change);
                    break;
                case "CashAccount":
                    ProcessCashAccount(change);
                    break;
            }
        }

        private static void ProcessCashAccount(SyncChunkResponseParameters change)
        {
            switch (change.Column)
            {
                case "Id":
                    isSyncSuccess = CashAccountRepository.AddSyncCashAccount(change.RowId, change.USN);
                    break;
                case "Name":
                    isSyncSuccess = CashAccountRepository.UpdateSyncCashAccount(change.RowId, change.USN, name: change.Value);
                    break;
                case "Amount":
                    isSyncSuccess = CashAccountRepository.UpdateSyncCashAccount(change.RowId, change.USN, amount: change.Value);
                    break;
                case "CurrencyId":
                    isSyncSuccess = CashAccountRepository.UpdateSyncCashAccount(change.RowId, change.USN, currencyId: change.Value);
                    break;
                case "AccountId":
                    isSyncSuccess = CashAccountRepository.UpdateSyncCashAccount(change.RowId, change.USN, accountId: change.Value);
                    break;
                case null:
                    isSyncSuccess = CashAccountRepository.DeleteSyncCashAccount(change.RowId);
                    break;
                default:
                    isSyncSuccess = false;
                    break;
            }
        }

        private static void ProcessCashFlowCategory(SyncChunkResponseParameters change)
        {
            switch (change.Column)
            {
                case "Id":
                    isSyncSuccess = CashFlowCategoryRepository.AddSyncCashFlowCategory(change.RowId, change.USN);
                    break;
                case "Name":
                    isSyncSuccess = CashFlowCategoryRepository.UpdateSyncCashFlowCategory(change.RowId, change.USN, name: change.Value);
                    break;
                case "AccountId":
                    isSyncSuccess = CashFlowCategoryRepository.UpdateSyncCashFlowCategory(change.RowId, change.USN, accountId: change.Value);
                    break;
                case "ParrentCashflowCategoryId":
                    isSyncSuccess = CashFlowCategoryRepository.UpdateSyncCashFlowCategory(change.RowId, change.USN, parentId: change.Value);
                    break;
                case null:
                    isSyncSuccess = CashFlowCategoryRepository.DeleteSyncCashFlowCategory(change.RowId);
                    break;
                default:
                    isSyncSuccess = false;
                    break;
            }
        }

        private static void ProcessCashFlow(SyncChunkResponseParameters change)
        {
            switch (change.Column)
            {
                case "Id":
                    isSyncSuccess = CashFlowRepository.AddSyncCashFlow(change.RowId, change.USN);
                    break;
                case "AccountId":
                    isSyncSuccess = CashFlowRepository.UpdateSyncCashFlow(change.RowId, change.USN, accountId: change.Value);
                    break;
                case "Amount":
                    isSyncSuccess = CashFlowRepository.UpdateSyncCashFlow(change.RowId, change.USN, amount: change.Value);
                    break;
                case "Date":
                    isSyncSuccess = CashFlowRepository.UpdateSyncCashFlow(change.RowId, change.USN, date: change.Value);
                    break;
                case "Description":
                    isSyncSuccess = CashFlowRepository.UpdateSyncCashFlow(change.RowId, change.USN, description: change.Value);
                    break;
                case "CashAccountId":
                    isSyncSuccess = CashFlowRepository.UpdateSyncCashFlow(change.RowId, change.USN, cashAccountId: change.Value);
                    break;
                case "CashFlowCategoryId":
                    isSyncSuccess = CashFlowRepository.UpdateSyncCashFlow(change.RowId, change.USN, cashFlowCategoryId: change.Value);
                    break;
                case null:
                    isSyncSuccess = CashFlowRepository.DeleteSyncCashFlow(change.RowId);
                    break;
                default:
                    isSyncSuccess = false;
                    break;
            }
        }

        private static async Task SendCashAccount()
        {
            var listToSend = CashAccountRepository.GetModifiedCashAccounts();
            if (!listToSend.Any()) return;

            var listOfUpdatedtems = listToSend.Where(c => c.USN > 0);
            foreach (var item in listOfUpdatedtems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "AccountId", Value = item.AccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "Name", Value = item.Name });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "CurrencyId", Value = item.CurrencyId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "Amount", Value = item.Amount.ToString() });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                item.USN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.LastUSN = item.USN;
                Properties.App.Default.Save();

                if (!CashAccountRepository.UnflagCashAccount(item.Id, item.USN)) return;
            }

            var listOfDeleteItems = listToSend.Where(c => c.USN == -1);
            foreach (var item in listOfDeleteItems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.DeleteCashAccount, new { RowId = item.Id });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                CashAccountRepository.DeleteSyncCashAccount(item.Id);
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();
            }

            var listOfNewItems = listToSend.Where(c => c.USN == 0);
            foreach (var item in listOfNewItems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.CreateCashAccount, new { RowId = item.Id });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "AccountId", Value = item.AccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "Name", Value = item.Name });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "CurrencyId", Value = item.CurrencyId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashAccount, new { RowId = item.Id, Column = "Amount", Value = item.Amount.ToString() });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                item.USN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.LastUSN = item.USN;
                Properties.App.Default.Save();

                if (!CashAccountRepository.UnflagCashAccount(item.Id, item.USN)) return;
            }

            
        }

        private static async Task SendCashFlowCategory()
        {
            var listToSend = CashFlowCategoryRepository.GetModifiedCashFlowCategories();
            if (!listToSend.Any()) return;

            var listOfUpdatedtems = listToSend.Where(c => c.USN > 0);
            foreach (var item in listOfUpdatedtems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlowCategory, new { RowId = item.Id, Column = "AccountId", Value = item.AccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlowCategory, new { RowId = item.Id, Column = "Name", Value = item.Name });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlowCategory, new { RowId = item.Id, Column = "ParentCashflowCategoryId", Value = item.ParentCashflowCategoryId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                item.USN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                if (!CashFlowCategoryRepository.UnflagCashFlowCategory(item.Id, item.USN)) return;
            }

            var listOfDeleteItems = listToSend.Where(c => c.USN == -1);
            foreach (var item in listOfDeleteItems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.DeleteCashFlowCategory, new { RowId = item.Id });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                CashFlowCategoryRepository.DeleteSyncCashFlowCategory(item.Id);
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();
            }

            var listOfNewItems = listToSend.Where(c => c.USN == 0);
            foreach (var item in listOfNewItems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.CreateCashFlowCategory, new { RowId = item.Id });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlowCategory, new { RowId = item.Id, Column = "AccountId", Value = item.AccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlowCategory, new { RowId = item.Id, Column = "Name", Value = item.Name });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlowCategory, new { RowId = item.Id, Column = "ParentCashflowCategoryId", Value = item.ParentCashflowCategoryId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                item.USN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.LastUSN = item.USN;
                Properties.App.Default.Save();


                if (!CashFlowCategoryRepository.UnflagCashFlowCategory(item.Id, item.USN)) return;
            }
        }

        private static async Task SendCashFlow()
        {
            var listToSend = CashFlowRepository.GetModifiedCashFlows();
            if (!listToSend.Any()) return;

            var listOfUpdatedtems = listToSend.Where(c => c.USN > 0);
            foreach (var item in listOfUpdatedtems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "AccountId", Value = item.AccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "Description", Value = item.Description });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "Date", Value = item.Date.ToString() });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "Amount", Value = item.Amount.ToString() });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "CashAccountId", Value = item.CashAccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "CashFlowCategoryId", Value = item.CashFlowCategoryId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                item.USN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.LastUSN = item.USN;
                Properties.App.Default.Save();

                if (!CashFlowRepository.UnflagCashFlow(item.Id, item.USN)) return;
            }

            var listOfDeleteItems = listToSend.Where(c => c.USN == -1);
            foreach (var item in listOfDeleteItems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.DeleteCashFlow, new { RowId = item.Id });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                CashFlowRepository.DeleteSyncCashFlow(item.Id);
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();
            }

            var listOfNewItems = listToSend.Where(c => c.USN == 0);
            foreach (var item in listOfNewItems)
            {
                var response = await ApiAuthService.PostAsync(ApiRequestEnum.CreateCashFlow, new { RowId = item.Id });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "AccountId", Value = item.AccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "Description", Value = item.Description });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "Date", Value = item.Date.ToString() });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "Amount", Value = item.Amount.ToString() });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "CashAccountId", Value = item.CashAccountId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                Properties.App.Default.LastUSN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.Save();

                response = await ApiAuthService.PostAsync(ApiRequestEnum.UpdateCashFlow, new { RowId = item.Id, Column = "CashFlowCategoryId", Value = item.CashFlowCategoryId });
                if (response != null && !response.IsSuccessStatusCode)
                {
                    MessageBoxExtension.ShowError(response);
                    return;
                }
                item.USN = response.Content.ReadAsAsync<SyncChunkResponseParameters>().Result.USN;
                Properties.App.Default.LastUSN = item.USN;
                Properties.App.Default.Save();

                if (!CashFlowRepository.UnflagCashFlow(item.Id, item.USN)) return;
            }
        }
    }
}