using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace Library_Management.DAL
{
    public class Categories
    {
        #region Basic Functionality

        #region Variable Declaration

        /// <summary>
        /// Variable to store Database object to interact with database.
        /// </summary>
        private Database db;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Categories class.
        /// </summary>
        public Categories()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the Categories class.
        /// </summary>
        /// <param name="CategoryId">Sets the value of TagId.</param>
        public Categories(int CategoryId)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.CategoryId = CategoryId;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CategoryId
        /// </summary>
        public int CategoryId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets CategoryName
        /// </summary>
        public string CategoryName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets IsActive
        /// </summary>
        public bool IsActive
        {
            get; set;
        }

        public int CreatedBy
        {
            get; set;
        }

        public DateTime CreatedOn
        {
            get; set;
        }

        public int ModifiedBy
        {
            get; set;
        }

        public DateTime ModifiedOn
        {
            get; set;
        }
        #endregion

        #region Actions

        public bool Load()
        {
            try
            {
                if (this.CategoryId != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("CategoriesGetDetails");
                    this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
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
            if (this.CategoryId == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.CategoryId > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.CategoryId = 0;
                    return false;
                }
            }
        }


        private bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("CategoriesInsert");
                this.db.AddOutParameter(com, "CategoryId", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.CategoryName))
                {
                    this.db.AddInParameter(com, "CategoryName", DbType.String, this.CategoryName);
                }
                else
                {
                    this.db.AddInParameter(com, "CategoryName", DbType.String, DBNull.Value);
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
                this.CategoryId = Convert.ToInt32(this.db.GetParameterValue(com, "TagId"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return this.CategoryId > 0; // Return whether ID was returned
        }

        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("CategoriesUpdate");
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
                if (!String.IsNullOrEmpty(this.CategoryName))
                {
                    this.db.AddInParameter(com, "CategoryName", DbType.String, this.CategoryName);
                }
                else
                {
                    this.db.AddInParameter(com, "CategoryName", DbType.String, DBNull.Value);
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
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public bool Delete()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("CategoriesDelete");
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<Categories> CategoriesGetList()
        {
            DataSet dts = null;
            List<Categories> categorieslist = new List<Categories>();
            try
            {
                DbCommand com = db.GetStoredProcCommand("CategoriesGetList");
                dts = db.ExecuteDataSet(com);
                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    categorieslist.Add(new Categories()
                    {
                        CategoryId = Convert.ToInt32(dts.Tables[0].Rows[i]["CategoryId"]),
                        CategoryName = dts.Tables[0].Rows[i]["CategoryName"].ToString(),
                        IsActive = Convert.ToBoolean(dts.Tables[0].Rows[i]["IsActive"]),
                        CreatedBy = Convert.ToInt32(dts.Tables[0].Rows[i]["CreatedBy"]),
                        CreatedOn = Convert.ToDateTime(dts.Tables[0].Rows[i]["CreatedOn"]),
                        ModifiedBy = Convert.ToInt32(dts.Tables[0].Rows[i]["ModifiedBy"]),
                        ModifiedOn = Convert.ToDateTime(dts.Tables[0].Rows[i]["ModifiedOn"])
                    });
                }
            }
            catch (Exception ex)
            {
                //To Do: Handle Exception
            }

            return categorieslist;
        }
        #endregion

        #endregion
    }
}