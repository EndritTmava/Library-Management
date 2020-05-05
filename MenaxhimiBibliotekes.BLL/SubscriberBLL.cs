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
    public class SubscriberBLL : ICreate<Subscriber>, IUpdate<Subscriber>, IDelete, IRead<Subscriber>
    {
        SubscriberDAL subscriberDAL = new SubscriberDAL();

        public int Add(Subscriber obj)
        {
            return subscriberDAL.Add(obj);
        }

        public int Delete(int Id)
        {
            return subscriberDAL.Delete(Id);
        }



        public Subscriber Get(int Id)
        {
            return Get(Id);
        }

        public Subscriber Get(Subscriber obj)
        {
            return Get(obj);
        }

        public List<Subscriber> GetAll()
        {
            return GetAll();
        }

        public int Update(Subscriber obj)
        {
            return subscriberDAL.Update(obj);
        }
    }
}
