using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Library_Management.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.DAL
{
    public class Books
    {
        #region Basic Functionality

        #region Variable Declaration
        private Database db;
        #endregion

        #region Constructors
        public Books()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }

        public Books(int BookId)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.BookId = BookId;
        }
        #endregion

        #region Properties
        
        public int BookId {get; set;}
        //[Required]
        public string BookName { get; set; }
        //public string BookDescription { get; set; }
        public int BookQuantity { get; set; }
        public List<Books> bookslist { get; set; }
        //[Required]
        public string PublisherId { get; set;  }
        //[Required]
        public string PublisherName  {  get; set; }
        public List<Publishers> publisherslist { get; set; }
        //[Required]
        public string CategoryId { get; set; }
        //[Required]
        public string CategoryName {  get; set; }
        public List<Categories> categorieslist { get; set; }
        public bool IsActive  { get; set;}
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set;  }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Actions

        public bool Load()
        {
            try
            {
                if (this.BookId != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("BooksGetDetails");
                    this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.BookId = Convert.ToInt32(dt.Rows[0]["BookId"]);
                        this.BookName = Convert.ToString(dt.Rows[0]["BookName"]);
                        this.BookQuantity = Convert.ToInt32(dt.Rows[0]["BookQuantity"]);
                        //this.BookDescription = Convert.ToString(dt.Rows[0]["BookDescription"]);
                        //this.PublisherId = Convert.ToString(dt.Rows[0]["PublisherId"]);
                        //this.PublisherId = Convert.ToInt32(dt.Rows[0]["PublisherId"]);
                        this.PublisherName = Convert.ToString(dt.Rows[0]["PublisherName"]);
                        //this.CategoryId = Convert.ToString(dt.Rows[0]["CategoryId"]);
                        //this.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                        this.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                        this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                        this.CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]);
                        this.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                        this.ModifiedBy = Convert.ToInt32(dt.Rows[0]["ModifiedBy"]);
                        this.ModifiedOn = Convert.ToDateTime(dt.Rows[0]["ModifiedOn"]);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
        }

        public bool Save()
        {
            if (this.BookId == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.BookId > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.BookId = 0;
                    return false;
                }
            }
        }

        public bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("BooksInsert");
                this.db.AddOutParameter(com, "BookId", DbType.Int32, 1024);

                if (!String.IsNullOrEmpty(this.BookName)){
                    this.db.AddInParameter(com, "BookName", DbType.String, this.BookName);
                } else {
                    this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
                }

                if (this.BookQuantity > 0)
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, this.BookQuantity);
                }
                else
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, DBNull.Value);
                }

                //if (!String.IsNullOrEmpty(this.BookDescription)){
                //    this.db.AddInParameter(com, "BookDescription", DbType.String, this.BookDescription);
                //} else {
                //    this.db.AddInParameter(com, "BookDescription", DbType.String, DBNull.Value);
                //}

                if (!String.IsNullOrEmpty(this.PublisherId))
                {
                    this.db.AddInParameter(com, "PublisherId", DbType.String, this.PublisherId);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherId", DbType.String, DBNull.Value);
                }

                //if (this.PublisherId > 0){
                //    this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
                //} else {
                //    this.db.AddInParameter(com, "PublisherId", DbType.Int32, DBNull.Value);
                //}

                //if (!String.IsNullOrEmpty(this.PublisherName)) {
                //    this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
                //} else {
                //    this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
                //}

                if (!String.IsNullOrEmpty(this.CategoryId))
                {
                    this.db.AddInParameter(com, "CategoryId", DbType.String, this.CategoryId);
                }
                else
                {
                    this.db.AddInParameter(com, "CategoryId", DbType.String, DBNull.Value);
                }

                //if (this.CategoryId > 0) {
                //    this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
                //}else {
                //    this.db.AddInParameter(com, "CategoryId", DbType.Int32, DBNull.Value);
                //}

                //if (!String.IsNullOrEmpty(this.CategoryName)) {
                //    this.db.AddInParameter(com, "CategoryName", DbType.String, this.CategoryName);
                //} else{
                //    this.db.AddInParameter(com, "CategoryName", DbType.String, DBNull.Value);
                //}

                this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

                if (this.CreatedBy > 0) {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                } else {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
                }

                if (this.CreatedOn > DateTime.MinValue) {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
                } else {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
                }

                if (this.ModifiedBy > 0) {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                } else {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
                }

                if (this.ModifiedOn > DateTime.MinValue) {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
                } else {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
                }

                this.db.ExecuteNonQuery(com);
                this.BookId = Convert.ToInt32(this.db.GetParameterValue(com, "BookId"));      // Read in the output parameter value
            }
            catch (Exception ex) {
                // To Do: Handle Exception
                return false;
            }
            return this.BookId > 0; // Return whether ID was returned
        }

        public bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("BooksUpdate");
                this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);

                if (!String.IsNullOrEmpty(this.BookName)) {
                    this.db.AddInParameter(com, "BookName", DbType.String, this.BookName);
                } else {
                    this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
                }

                if (this.BookQuantity > 0)
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, this.BookQuantity);
                }
                else
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, DBNull.Value);
                }

                //if (!String.IsNullOrEmpty(this.BookDescription)){
                //    this.db.AddInParameter(com, "BookDescription", DbType.String, this.BookDescription);
                //}else {
                //    this.db.AddInParameter(com, "BookDescription", DbType.String, DBNull.Value);
                //}

                if (!String.IsNullOrEmpty(this.PublisherId))
                {
                    this.db.AddInParameter(com, "PublisherId", DbType.String, this.PublisherId);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherId", DbType.String, DBNull.Value);
                }

                //if (this.PublisherId > 0){
                //    this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
                //} else {
                //    this.db.AddInParameter(com, "PublisherId", DbType.Int32, DBNull.Value);
                //}

                //if (!String.IsNullOrEmpty(this.PublisherName)) {
                //    this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
                //} else{
                //    this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
                //}

                if (!String.IsNullOrEmpty(this.CategoryId))
                {
                    this.db.AddInParameter(com, "CategoryId", DbType.String, this.CategoryId);
                }
                else
                {
                    this.db.AddInParameter(com, "CategoryId", DbType.String, DBNull.Value);
                }

                //if (this.CategoryId > 0){
                //    this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
                //} else {
                //    this.db.AddInParameter(com, "CategoryId", DbType.Int32, DBNull.Value);
                //}

                //if (!String.IsNullOrEmpty(this.CategoryName)) {
                //    this.db.AddInParameter(com, "CategoryName", DbType.String, this.CategoryName);
                //} else {
                //    this.db.AddInParameter(com, "CategoryName", DbType.String, DBNull.Value);
                //}

                this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

                if (this.CreatedBy > 0) {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                } else {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
                }

                if (this.CreatedOn > DateTime.MinValue) {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
                } else{
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
                }

                if (this.ModifiedBy > 0) {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                } else {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
                }

                if (this.ModifiedOn > DateTime.MinValue) {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
                } else{
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
                }

                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex){
                // To Do: Handle Exception
                return false;
            }
            return true;
        }

        public bool Delete()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("BooksDelete");
                this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex){
                // To Do: Handle Exception
                return false;
            }
            return true;
        }

        public List<Books> BooksGetList()
        {
            DataSet dts = null;
            List<Books> booklist = new List<Books>();
            try
            {
                DbCommand com = db.GetStoredProcCommand("BooksGetList");
                if (!String.IsNullOrEmpty(this.BookName)) {
                    this.db.AddInParameter(com, "BookName", DbType.String, this.BookName);
                }else {
                    this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
                }
                if (this.BookQuantity > 0)
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, this.BookQuantity);
                }
                else
                {
                    this.db.AddInParameter(com, "BookQuantity", DbType.Int32, DBNull.Value);
                }

                if (!String.IsNullOrEmpty(this.CategoryId))
                {
                    this.db.AddInParameter(com, "CategoryId", DbType.String, this.CategoryId);
                }
                else
                {
                    this.db.AddInParameter(com, "CategoryId", DbType.String, DBNull.Value);
                }

                //if (this.CategoryId > 0) {
                //    this.db.AddInParameter(com, "CategoryId", DbType.String, this.CategoryId);
                //}else {
                //    this.db.AddInParameter(com, "CategoryId", DbType.String, DBNull.Value);
                //}

                if (!String.IsNullOrEmpty(this.PublisherId))
                {
                    this.db.AddInParameter(com, "PublisherId", DbType.String, this.PublisherId);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherId", DbType.String, DBNull.Value);
                }

                //if (this.PublisherId > 0) {
                //    this.db.AddInParameter(com, "PublisherId", DbType.String, this.PublisherId);
                //} else {
                //    this.db.AddInParameter(com, "PublisherId", DbType.String, DBNull.Value);
                //}

                if (this.PageNumber > 0) {
                    this.db.AddInParameter(com, "PageNumber", DbType.Int32, this.PageNumber);
                } else {
                    this.db.AddInParameter(com, "PageNumber", DbType.Int32, 1);
                }

                if (this.PageSize > 0){
                    this.db.AddInParameter(com, "PageSize", DbType.Int32, this.PageSize);
                } else {
                    this.db.AddInParameter(com, "PageSize", DbType.Int32, 5);
                }

                this.db.AddOutParameter(com, "TotalRecords", DbType.Int32, 10);

                dts = db.ExecuteDataSet(com);

                this.TotalRecords = Convert.ToInt32(this.db.GetParameterValue(com, "TotalRecords"));

                //booklist = new List<BooksClass>();

                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    booklist.Add(new Books()
                    {
                        BookId = Convert.ToInt32(dts.Tables[0].Rows[i]["BookId"]),
                        BookName = dts.Tables[0].Rows[i]["BookName"].ToString(),
                        BookQuantity = Convert.ToInt32(dts.Tables[0].Rows[i]["BookQuantity"]),
                        //BookDescription = dts.Tables[0].Rows[i]["BookDescription"].ToString(),             
                        PublisherName = dts.Tables[0].Rows[i]["PublisherName"].ToString(),
                        CategoryName = dts.Tables[0].Rows[i]["CategoryName"].ToString(),
                        TotalRecords = Convert.ToInt32(dts.Tables[0].Rows[i]["TotalRecords"]),
                    });
                }
            }
            catch (Exception ex){
                //To Do: Handle Exception
            }   
            return booklist;
        }
        #endregion

        #endregion
    }
}