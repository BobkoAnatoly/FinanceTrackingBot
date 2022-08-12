using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackingBot.Common.Enums
{
    public class CommandNames
    {
        public const string StartCommand = "/start";
        public const string AddOperationCommand = "add-operation";
        public const string FinishOperationCommand = "finish-operation";
        public const string SelectCategoryCommand = "select-category";
        public const string GetOperationsCommand = "get-operations";
        public const string SelectAnalyticDaysCommand = "select-analytic-days";
        public const string GetAnalyticsCommand = "get-analytics";
    }
}
