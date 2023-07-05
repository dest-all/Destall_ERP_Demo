//#define Ru

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Common;

public static partial class Phrases
{

    public static class Custom
    {
#if Ru
        public const string Pending_Payments = "Непогашенные счета";
        public const string Reports = "Отчеты";
        public const string Application = "Приложение";

        public const string ApplyCompression = "Применять сжатие (предпочтительно)";
        public const string UseEncoding = "Применять шифрование (предпочтительно)";
        public const string Settings = "Настройки";
        public const string Data = "Данные";

        public static class FinancialReports
        {
            public const string AccountPayable = "Кредиторская задолженность";
            public const string AccountsReceivable = "Дебиторская задолженность";
            public const string NoDueIncomingPayments = "Нет дебиторской задолженности";
            public const string NoDebtsToPay = "Нет кредиторской задолженности";

            public const string TotalSum = "Всего";
            public const string Settled = "Погашено";
            public const string Left = "Осталось";
            public const string RelatedPayments = "Связанные платежи";
        }

        public static class Auth
        {
            public const string Login = "Логин";
            public const string OldPassword = "Текущий пароль";
            public const string NewPassword = "Новый пароль";
            public const string Change = "Изменить";
            public const string ChangePassword = "Сменить пароль";
            public const string LoggedInAs = "Авторизован как {0}";
            public const string SignIn = "Войти";
            public const string SignOut = "Выйти";
            public const string Password = "Пароль";

            public const string InvalidCredentialsSupplied = "Неверные учетные данные";
        }

#else
        public const string Pending_Payments = "Pending Payments";
        public const string Reports = "Reports";
        public const string Application = "Application";

        public const string ApplyCompression = "Apply compression (preferrable)";
        public const string UseEncoding = "Use encoding (preferrable)";
        public const string Settings = "Settings";
        public const string Data = "Data";
        
        public static class FinancialReports
        {
            public const string NoDueIncomingPayments = "No due incoming payments";
            public const string NoDebtsToPay = "No debts to pay";
            public const string AccountPayable = "Accounts payable";
            public const string AccountsReceivable = "Accounts receivable";

            public const string TotalSum = "Total";
            public const string Settled = "Settled";
            public const string Left = "Left";
            public const string RelatedPayments = "Related payments";
        }

        public static class Auth
        {
            public const string Login = "Login";
            public const string OldPassword = "Current password";
            public const string NewPassword = "New password";
            public const string Change = "Change";
            public const string ChangePassword = "Change password";
            public const string LoggedInAs = "Logged in as {0}";
            public const string SignIn = "Sign in";
            public const string SignOut = "Sign out";
            public const string Password = "Password";

            public const string InvalidCredentialsSupplied = "Invalid credentials supplied";
        }
#endif
    }
}
