using DataTableToIEnumerable.Extensions;
using DataTableToIEnumerable.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataTableToIEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            // Convert from a DataTable source to an IEnumerable.
            var usersSourceDataTable = CreateMockUserDataTable();
            var usersConvertedList = usersSourceDataTable.ToEnumerable<User>();

            // Convert from an IEnumerable source to a DataTable.
            var usersSourceList = CreateMockUserList();
            var usersConvertedDataTable = usersSourceList.ToDataTable<User>();
        }

        private static DataTable CreateMockUserDataTable()
        {
            var usersDataTable = new DataTable("USER_TABLE");
            usersDataTable.Columns.Add("UserNameCode");
            usersDataTable.Columns.Add("GroupCode");

            DataRow newRow;
            newRow = usersDataTable.NewRow();
            newRow["UserNameCode"] = "StephanArnas";
            newRow["GroupCode"] = "Admin";
            usersDataTable.Rows.Add(newRow);

            newRow = usersDataTable.NewRow();
            newRow["UserNameCode"] = "JohnSmith";
            newRow["GroupCode"] = "User";
            usersDataTable.Rows.Add(newRow);

            return usersDataTable;
        }

        private static IEnumerable<User> CreateMockUserList()
        {
            var usersList = new List<User>();

            usersList.Add(new User() { UserName = "StephanArnas", Groups = "Admin" });
            usersList.Add(new User() { UserName = "JohnSmith", Groups = "User" });

            return usersList;
        }
    }
}
