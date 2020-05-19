﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenaxhimiBibliotekes.BO.Interfaces;
using MenaxhimiBibliotekes.BO;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MenaxhimiBibliotekes.DAL
{
    public class LanguageDAL : ICreate<Language>, IUpdate<Language>, IDelete, IRead<Language>, IConvertToBO<Language>
    {
        Language lang;
        public int Add(Language obj)
        {
            int isInserted = 0;
            try
            {
                int error;
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    using (SqlCommand command = DbHelper.Command(conn, "usp_CreateLanguage", CommandType.StoredProcedure))
                    {
                        command.Parameters.AddWithValue("Language", obj._Language);
                        command.Parameters.AddWithValue("InsBy", obj.InsBy);
                       

                        SqlParameter sqlpa = new SqlParameter();
                        sqlpa.ParameterName = "Error";
                        sqlpa.SqlDbType = SqlDbType.Int;
                        sqlpa.Direction = ParameterDirection.Output;

                        command.Parameters.Add(sqlpa);

                        isInserted= command.ExecuteNonQuery();
                        error = (int)sqlpa.Value;
                        return error;
                    }
                }
            }


            catch (Exception)
            {
                
                return -1;
            }
        }

        public int Delete(int Id)
        {
            int IsDeleted = 0;
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    using (SqlCommand command = DbHelper.Command(conn, "usp_DeleteLanguage", CommandType.StoredProcedure))
                    {
                        command.Parameters.AddWithValue("LanguageId", Id);
                        IsDeleted = command.ExecuteNonQuery();

                        

                        if (IsDeleted > 0)
                        {
                            return 0;
                        }


                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception)
            {

                return -1;
            }
        }


        public Language Get(int Id)
        {

            //lang = new Language();

            //using (SqlConnection sqlconn = DbHelper.GetConnection())
            //{
            //    using (SqlCommand command = DbHelper.Command(sqlconn, "usp_GetLanguage", CommandType.StoredProcedure))
            //    {
            //        using (SqlDataReader sqr = command.ExecuteReader())
            //        {

            //                if (sqr.Read())
            //                {

            //                    lang = ToBO(sqr);
            //                }

            //            return lang;
            //        }
            //    }
            //}
            throw new NotImplementedException();
        }



        public List<Language> GetAll()
        {
            try
            {
                List<Language> AllLanguage = new List<Language>();
                lang = new Language();

                using (SqlConnection sqlconn = DbHelper.GetConnection())
                {
                    using (SqlCommand command = DbHelper.Command(sqlconn, "usp_GetAllLanguages", CommandType.StoredProcedure))
                    {
                        using (SqlDataReader sqr = command.ExecuteReader())
                        {
                            if (sqr.HasRows)
                            {
                                while (sqr.Read())
                                {

                                    lang = ToBO(sqr);
                                    if (lang == null)
                                    {
                                        throw new Exception();
                                    }



                                    AllLanguage.Add(lang);



                                }
                            }
                            return AllLanguage;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("There was a problem, please contact your administrator");
                return null;
            }


        }
        public int Update(Language obj)
        {
            int isUpdated = 0;
            try
            {
                int error;

                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    using (SqlCommand command = DbHelper.Command(conn, "usp_UpdateLanguage", CommandType.StoredProcedure))
                    {
                        command.Parameters.AddWithValue("LanguageId", obj.LanguageId);
                        command.Parameters.AddWithValue("Language", obj._Language);
                        command.Parameters.AddWithValue("UpdBy", obj.UpdBy);




                        isUpdated = command.ExecuteNonQuery();
                        if (isUpdated > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }

                    }
                }
            }
        

            catch (SqlException ex)
            {

                MessageBox.Show("Please contact your administrator");
                return -1;

            }

            catch (Exception)
            {
                MessageBox.Show("Material Type name  should be uniqe, please if this material type is deactivated update it");
                return -1;
            }
        }

        public Language ToBO(SqlDataReader reader)
        {

            try
            {
                lang = new Language();

                lang.LanguageId = (int)reader["LanguageId"];
                lang._Language = reader["Language"].ToString();

                lang.InsBy = int.Parse(reader["InsBy"].ToString());
                lang.InsDate = (DateTime)reader["InsDate"];

                if (reader["UpdBy"] != DBNull.Value)
                {
                    lang.UpdBy = int.Parse(reader["UpdBy"].ToString());
                }
                if (reader["UpdDate"] != DBNull.Value)
                {
                    lang.UpdDate = (DateTime)reader["UpdDate"];
                }

                lang.UpdNo = int.Parse(reader["UpdNo"].ToString());
                return lang;
            }





            catch (Exception)
            {

                throw;
            }
        }



    }


    
}
