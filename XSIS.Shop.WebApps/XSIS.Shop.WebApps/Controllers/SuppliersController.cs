﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XSIS.Shop.Repository;
using XSIS.Shop.ViewModels;

namespace XSIS.Shop.WebApps.Controllers
{
    public class SuppliersController : Controller
    {

        private SupplierRepository service = new SupplierRepository();

        [HttpGet]
        public ActionResult Index(string FullName, string CityCountry, string PhoneNo)
        {
            List<SupplierViewModel> list = null;
            var result = list;

            if (!string.IsNullOrEmpty(FullName) || !string.IsNullOrEmpty(CityCountry) || !string.IsNullOrEmpty(PhoneNo) ||
                !string.IsNullOrWhiteSpace(FullName) || !string.IsNullOrWhiteSpace(CityCountry) || !string.IsNullOrWhiteSpace(PhoneNo)
                || FullName != " " || CityCountry != " " || PhoneNo != " ")

            {
                result = service.SearchByKey(FullName, CityCountry, PhoneNo);
            }
            else
            {
                result = service.GetAllSupplier();
            }

            return View(result.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int idx = id.HasValue ? id.Value : 0;
            SupplierViewModel supplier = service.GetDetailSupplierById(idx);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                service.AddNewSupplier(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int idx = id.HasValue ? id.Value : 0;
            SupplierViewModel supplier = service.GetSupplierById(idx);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel supplier)
        {
            if (ModelState.IsValid)
            {
                service.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int idx = id.HasValue ? id.Value : 0;
            SupplierViewModel supplier = service.GetSupplierById(idx);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteSupplier(id);
            return RedirectToAction("Index");
        }
    }
}
