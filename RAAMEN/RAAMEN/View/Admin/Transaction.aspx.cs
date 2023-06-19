﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RAAMEN.Model;
using RAAMEN.Repository;

namespace RAAMEN.View.Admin
{
    public partial class Transaction : System.Web.UI.Page
    {
        public List<Header> listOrder = new List<Header>();
        public List<List<Detail>> listDetailOrder = new List<List<Detail>>();

        protected void Page_Load(object sender, EventArgs e)
        {

            listOrder = OrderRepository.getListOrderByStatus(true);

            foreach (Header order in listOrder)
            {
                List<Detail> detailOrder = new List<Detail>();
                detailOrder = OrderRepository.getDetailOrderById(order.Id);

                listDetailOrder.Add(detailOrder);
            }
        }
    }
}