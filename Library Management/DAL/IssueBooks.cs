using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Library_Management.Models;

namespace Library_Management.DAL
{
    public class IssueBooks
    {
        #region Basic Functionality

        #region Variable Declaration
        private Database db;
        #endregion

        #region Constructors
        public IssueBooks()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }

        public IssueBooks(int bookId)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.BookId = bookId;
        }
        #endregion

        #region Properties
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int BookQuantity { get; set; }
        public string PublisherId { get; set; }
        public string CategoryId { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        #endregion

        #region Actions
        public bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("IssueInsert");
                this.db.AddOutParameter(com, "IssueId", DbType.Int32, 1024);

                if (this.BookId > 0)
                {
                    this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
                }
                else
                {
                    this.db.AddInParameter(com, "BookId", DbType.Int32, DBNull.Value);
                }

                if (this.BookQuantity > 0)
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, this.BookQuantity);
                }
                else
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, DBNull.Value);
                }

                if (this.IssueDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "IssueDate", DbType.DateTime, this.IssueDate);
                }
                else
                {
                    this.db.AddInParameter(com, "IssueDate", DbType.DateTime, DBNull.Value);
                }

                if (this.UserId > 0)
                {
                    this.db.AddInParameter(com, "UserId", DbType.Int32, this.UserId);
                }
                else
                {
                    this.db.AddInParameter(com, "UserId", DbType.Int32, DBNull.Value);
                }         
                this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

                if (this.CreatedBy > 0)
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
                }

                if (this.CreatedOn > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
                }

                if (this.ModifiedBy > 0)
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
                }

                if (this.ModifiedOn > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
                }

                this.db.ExecuteNonQuery(com);
                this.IssueId = Convert.ToInt32(this.db.GetParameterValue(com, "IssueId"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
            return this.IssueId > 0; // Return whether ID was returned
        }

        public List<IssueBooks> IssueBookGetList()
        {
            DataSet dsBooksList = null;
            List<IssueBooks> IssueBookList = null;
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("IssueBooksGetList");
                this.db.AddInParameter(com, "IssueId", DbType.Int32, this.IssueId);
                this.db.ExecuteNonQuery(com);
                dsBooksList = db.ExecuteDataSet(com);
                IssueBookList = new List<IssueBooks>();
                for (int i = 0; i < dsBooksList.Tables[0].Rows.Count; i++)
                {
                    IssueBookList.Add(new IssueBooks()
                    {
                        IssueId = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["IssueId"]),
                        UserId = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["UserId"]),
                        IssueDate = Convert.ToDateTime(dsBooksList.Tables[0].Rows[i]["IssueDate"]),
                        BookQuantity = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["BookQuantity"]),
                        BookId = Convert.ToInt32(dsBooksList.Tables[0].Rows[i]["BookId"]),
                        BookName = dsBooksList.Tables[0].Rows[i]["BookName"].ToString(),
                        CategoryName = dsBooksList.Tables[0].Rows[i]["CategoryName"].ToString(),
                        PublisherName = dsBooksList.Tables[0].Rows[i]["PublisherName"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                //To Do: Handle Exception
            }

            return IssueBookList;
        }

        public List<IssueBooks> InsertBookList()
        {
            DataSet dsInsertBooksList = null;
            List<IssueBooks> InsertList = null;
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("IssueBooksGetList");
                this.db.AddInParameter(com, "IssueId", DbType.Int32, this.IssueId);
                this.db.ExecuteNonQuery(com);
                dsInsertBooksList = db.ExecuteDataSet(com);
                InsertList = new List<IssueBooks>();
                for (int i = 0; i < dsInsertBooksList.Tables[0].Rows.Count; i++)
                {
                    InsertList.Add(new IssueBooks()
                    {
                        IssueId = Convert.ToInt32(dsInsertBooksList.Tables[0].Rows[i]["IssueId"]),
                        UserId = Convert.ToInt32(dsInsertBooksList.Tables[0].Rows[i]["UserId"]),
                        IssueDate = Convert.ToDateTime(dsInsertBooksList.Tables[0].Rows[i]["IssueDate"]),
                        BookQuantity = Convert.ToInt32(dsInsertBooksList.Tables[0].Rows[i]["BookQuantity"]),
                        BookId = Convert.ToInt32(dsInsertBooksList.Tables[0].Rows[i]["BookId"]),
                    });
                }
            }
            catch (Exception ex)
            {
                //To Do: Handle Exception
            }

            return InsertList;
        }
        #endregion

        #endregion
    }
}