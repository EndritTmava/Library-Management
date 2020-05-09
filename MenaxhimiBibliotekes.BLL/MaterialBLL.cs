﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenaxhimiBibliotekes.BO.Interfaces;
using MenaxhimiBibliotekes.BO;
using MenaxhimiBibliotekes.DAL;


namespace MenaxhimiBibliotekes.BLL
{
    public class MaterialBLL : ICreate<Material>, IUpdate<Material>, IDelete, IRead<Material>
    {
        MaterialDAL _materialDAL = new MaterialDAL();
        public int Add(Material obj)
        {
           return _materialDAL.Add(obj);
        }

        public int Delete(int Id)
        {
           return _materialDAL.Delete(Id);
        }


        public Material Get(int Id)
        {
            return _materialDAL.Get(Id);
        }


        public List<Material> GetAll()
        {
            return GetAll();
        }

        public int Update(Material obj)
        {
            return _materialDAL.Update(obj);
        }
    }
}
