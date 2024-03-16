using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;



//Add code logic that will calculate a quote based on the information the user inputs into the form.


//Complete these actions:


//In the InsureeController, add logic to calculate a quote based on these guidelines: 

//Start with a base of $50 / month.

//If the user is 18 or under, add $100 to the monthly total.

//If the user is from 19 to 25, add $50 to the monthly total.

//If the user is 26 or older, add $25 to the monthly total. Double check your code to ensure all ages are covered.

//If the car's year is before 2000, add $25 to the monthly total.

//If the car's year is after 2015, add $25 to the monthly total.

//If the car's Make is a Porsche, add $25 to the price.

//If the car's Make is a Porsche and its model is a 911 Carrera, add an additional $25 to the price. (Meaning, this specific car will add a total of $50 to the price.)

//Add $10 to the monthly total for every speeding ticket the user has.

//If the user has ever had a DUI, add 25% to the total.

//If it's full coverage, add 50% to the total.
namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public int CoverageCalculator(DateTime DateOfBirth, string CarMake, int CarYear,string CarModel, int SpeedingTickets, bool DUI, bool CoverageType)
        {
            //Start with a base of $50 / month.
            int InsCoverage = 50;
       
            //Get age of applicant 
            DateTime Now = DateTime.Now;
            var ApplicantAge = (DateTime.Today - DateOfBirth).Days / 365;

            //If the user is 18 or under, add $100 to the monthly total.
            if (ApplicantAge < 18)
            {
                InsCoverage += InsCoverage + 100;
                
            }
            //If the user is from 19 to 25, add $50 to the monthly total.
            if (ApplicantAge > 18 && ApplicantAge < 25)
            {
                InsCoverage += InsCoverage + 50;
                
            }
            //If the user is 26 or older, add $25 to the monthly total. Double check your code to ensure all ages are covered.
            if (ApplicantAge > 26)
            {
                 InsCoverage += InsCoverage + 25;
                
            }

            if (CarYear > 2015)
            {
                InsCoverage += InsCoverage + 25;
            }
            if (CarMake == "Porsche")
            {
                InsCoverage += InsCoverage + 25;
            }
            if (CarModel == "911 Carrera")
            {
                InsCoverage += InsCoverage + 25;
            }
             if (SpeedingTickets >0)
             {
                InsCoverage += InsCoverage + (SpeedingTickets * 10);
             }
             if (DUI == true)
             {
                InsCoverage += (InsCoverage + (InsCoverage / 4));
             }
             if(CoverageType==true)
             {
                InsCoverage += (InsCoverage + (InsCoverage / 2));
             } 



            return InsCoverage;
        }


       
    }


}

       
          
              
                      
    
           


    

