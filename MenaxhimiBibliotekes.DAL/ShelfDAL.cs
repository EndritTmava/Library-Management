﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenaxhimiBibliotekes.BO.Interfaces;
using MenaxhimiBibliotekes.BO;
using System.Data.SqlClient;
using System.Data;

namespace MenaxhimiBibliotekes.DAL
{
    public class ShelfDAL : ICrud<Shelf>, IConvertToBO<Shelf>
    {





        public bool Add(Shelf obj)
        {
            int isInserted = 0;
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    using (SqlCommand command = DbHelper.Command(conn, "usp_CreateShelf", CommandType.StoredProcedure))
                    {
                        command.Parameters.AddWithValue("Location", obj.Location);
                        command.Parameters.AddWithValue("Quantity", obj.Quantity);
                        command.Parameters.AddWithValue("Description", obj.Description);
                        command.Parameters.AddWithValue("InsBy", obj.InsBy);

                        isInserted = command.ExecuteNonQuery();


                        if (isInserted > 0)
                        {
                            return true;
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

                return false;
            }
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }



        public Shelf Get(int Id)
        {
            using (var conn = DbHelper.GetConnection())
            {
                using (var command = DbHelper.Command(conn, "usp_GetShelfById", CommandType.StoredProcedure))
                {
                    command.Parameters.AddWithValue("ShelfId", Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            return ToBO(reader);

                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }


        }


        public List<Shelf> GetAll()
        {
            List<Shelf> shelves = new List<Shelf>();
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    using (var command = DbHelper.Command(conn, "usp_GetShelfAllShelves", CommandType.StoredProcedure))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    shelves.Add(ToBO(reader));
                                }

                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                    }
                }
                return shelves;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Shelf ToBO(SqlDataReader reader)
        {
            try
            {
                Shelf shelf = new Shelf();

                shelf.ShelfId = int.Parse(reader["ShelfId"].ToString());
                shelf.Location = reader["Location"].ToString();
                shelf.Quantity = int.Parse(reader["Quantity"].ToString());

                if (reader["Description"] != DBNull.Value)
                {
                    shelf.Description = reader["Description"].ToString();
                }

                shelf.InsBy = int.Parse(reader["InsBy"].ToString());
                shelf.InsDate = (DateTime)reader["InsDate"];

                if (reader["UpdBy"] != DBNull.Value)
                {
                    shelf.UpdBy = int.Parse(reader["UpdBy"].ToString());
                }
                if (reader["UpdDate"] != DBNull.Value)
                {
                    shelf.UpdDate = (DateTime)reader["UpdDate"];
                }

                    shelf.UpdNo = int.Parse(reader["UpdNo"].ToString());
               

                return shelf;
            }
            catch (Exception)
            {

                return null ;
            }
        }

        public bool Update(Shelf obj)
        {
            int isUpdated = 0;
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    using (SqlCommand command = DbHelper.Command(conn, "usp_UpdateShelf", CommandType.StoredProcedure))
                    {
                        command.Parameters.AddWithValue("ShelfId", obj.ShelfId);
                        command.Parameters.AddWithValue("Location", obj.Location);
                        command.Parameters.AddWithValue("Quantity", obj.Quantity);
                        command.Parameters.AddWithValue("Description", obj.Description);
                        command.Parameters.AddWithValue("UpdBy", obj.UpdBy);

                        isUpdated = command.ExecuteNonQuery();


                        if (isUpdated > 0)
                        {
                            return true;
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

                return false;
            }
        }



    }

}

