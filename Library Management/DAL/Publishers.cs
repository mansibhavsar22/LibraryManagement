using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;

namespace Library_Management.DAL
{
    public class Publishers
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
        /// Initializes a new instance of the Publishers class.
        /// </summary>
        public Publishers()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the Publishers class.
        /// </summary>
        /// <param name="PublisherId">Sets the value of PublisherId.</param>
        public Publishers(int PublisherId)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.PublisherId = PublisherId;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets PublisherId
        /// </summary>
        public int PublisherId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets PublisherName
        /// </summary>
        public string PublisherName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets PublishDate
        /// </summary>
        public DateTime PublishDate
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

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        public int CreatedBy
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets CreatedOn
        /// </summary>
        public DateTime CreatedOn
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets ModifiedBy
        /// </summary>
        public int ModifiedBy
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets ModifiedOn
        /// </summary>
        public DateTime ModifiedOn
        {
            get; set;
        }
        #endregion

        #region Actions

        /// <summary>
        /// Loads the details for Publishers.
        /// </summary>
        /// <returns>True if Load operation is successful; Else False.</returns>
        public bool Load()
        {
            try
            {
                if (this.PublisherId != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("PublishersGetDetails");
                    this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.PublisherId = Convert.ToInt32(dt.Rows[0]["PublisherId"]);
                        this.PublisherName = Convert.ToString(dt.Rows[0]["PublisherName"]);
                        this.PublishDate = Convert.ToDateTime(dt.Rows[0]["PublishDate"]);
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

        /// <summary>
        /// Inserts details for Publishers if PublisherId = 0.
        /// Else updates details for Publishers.
        /// </summary>
        /// <returns>True if Save operation is successful; Else False.</returns>
        public bool Save()
        {
            if (this.PublisherId == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.PublisherId > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.PublisherId = 0;
                    return false;
                }
            }
        }

        /// <summary>
        /// Inserts details for Publishers.
        /// Saves newly created Id in Publishers.
        /// </summary>
        /// <returns>True if Insert operation is successful; Else False.</returns>
        private bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersInsert");
                this.db.AddOutParameter(com, "PublisherId", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.PublisherName))
                {
                    this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
                }
                if (this.PublishDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "PublishDate", DbType.DateTime, this.PublishDate);
                }
                else
                {
                    this.db.AddInParameter(com, "PublishDate", DbType.DateTime, DBNull.Value);
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
                this.PublisherId = Convert.ToInt32(this.db.GetParameterValue(com, "PublisherId"));      // Read in the output parameter value
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return this.PublisherId > 0; // Return whether ID was returned
        }

        /// <summary>
        /// Updates details for Publishers.
        /// </summary>
        /// <returns>True if Update operation is successful; Else False.</returns>
        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersUpdate");
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
                if (!String.IsNullOrEmpty(this.PublisherName))
                {
                    this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
                }
                else
                {
                    this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
                }
                if (this.PublishDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "PublishDate", DbType.DateTime, this.PublishDate);
                }
                else
                {
                    this.db.AddInParameter(com, "PublishDate", DbType.DateTime, DBNull.Value);
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

        /// <summary>
        /// Deletes details of Publishers for provided PublisherId.
        /// </summary>
        /// <returns>True if Delete operation is successful; Else False.</returns>
        public bool Delete()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersDelete");
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get list of Publishers for provided parameters.
        /// </summary>
        /// <returns>DataSet of result</returns>
        /// <remarks></remarks>
        public List<Publishers> PublishersGetList()
        {
            DataSet dts = null;
            List<Publishers> publisherslist = new List<Publishers>();
            try
            {
                DbCommand com = db.GetStoredProcCommand("PublishersGetList");
                dts = db.ExecuteDataSet(com);
                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    publisherslist.Add(new Publishers()
                    {
                        PublisherId = Convert.ToInt32(dts.Tables[0].Rows[i]["PublisherId"]),
                        PublisherName = dts.Tables[0].Rows[i]["PublisherName"].ToString(),
                        PublishDate = Convert.ToDateTime(dts.Tables[0].Rows[i]["PublishDate"]),
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

            return publisherslist;
        }
        #endregion

        #endregion
    }
}