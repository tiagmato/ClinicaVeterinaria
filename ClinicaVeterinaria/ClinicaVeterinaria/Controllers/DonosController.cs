using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize]// força a que só os utilizadores autentificados consigam aceder aos métodos desta classe.
    //aplica-se a todos os métodos
    public class DonosController : Controller
    {
        private VetsDB db = new VetsDB();

        // GET: Donos
        //[AllowAnonymous]//permite o acesso de utilizadores anónimos ao conteúdo deste método. Apenas este.
        public ActionResult Index()
        {
            //mostra os dados apenas para os Funcionarios ou para os Veterinarios
            if(User.IsInRole("Veterinario") || User.IsInRole("Funcionario")) {
                return View(db.Donos.ToList().OrderBy(d=>d.Nome));
            }
            //Se chegar aqui, é porque é DONO
            ///extraí apenas os dados do dono que se autentificou
            return View(db.Donos.Where(d=>d.UserName == User.Identity.Name).ToList());
        }

        // GET: Donos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);

            //criar um objeto do tipo ICollection e associar esse objeto ao "Dono" donos.ListaDeAnimais=.............


            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // GET: Donos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,NIF")] Donos dono)
        {
            //determinar o ID a atribuir ao novo 'dono'

            int novoID = 0;

            try
            {
            novoID = db.Donos.Max(d => d.DonoID) + 1;
            }
            catch (Exception)
            {
                //não existe dados na BD
                // o Max  devolve Null
                novoID = 1;

            }
            //outra forma
            /* novoID = (from d in db.Donos
                       orderby d.DonoID descending
                       select d.DonoID
                       ).FirstOrDefault()+1;

             //outra forma
             novoID = db.Donos.OrderByDescending(d => d.DonoID).FirstOrDefault().DonoID + 1;
             */

            //atribuir o novo ID ao 'dono'
            dono.DonoID = novoID;
            try
            {
            if (ModelState.IsValid)//confronta se os dados a ser  introduzidos estão consistentes com o Model
            {
                //adicionar um novo dono
                db.Donos.Add(dono);
                //guardar as alterações
                db.SaveChanges();
                //redirecionar para a página dde início
                return RedirectToAction("Index");
            }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", string.Format("Ocorreu um erro na operação de guardar um novo Dono"));
                //adicionar a uma classe Erro(id, TimeStamp, Operação, mensagem de erro 'ex.Message', qual o user que gerou o erro) enviar emailao utilizador 'Admin' a avisar da ocorrenciado do erro
            }
            // se houver problemas volta pra a view do create com os dados do dono
            return View(dono);
        }

        // GET: Donos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // POST: Donos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonoID,Nome,NIF")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donos);
        }

        // GET: Donos/Delete/5
        public ActionResult Delete(int? id)
        {
            //se não foi fornecido o ID do 'Dono' ...
            if (id == null)
            {
                //redireciono o utilizador para a Lista de Donos
                return RedirectToAction("Index");
            }
            //vai à procura do 'Dono', cujo ID foi fornecido
            Donos dono = db.Donos.Find(id);

            //se o 'Dono' associado ao ID fornecido não existe ...
            if (dono == null)
            {
                //redireciono o utilizador para a Lista de Donos
                return RedirectToAction("Index");

            }
            //mostra dados do 'Dono'
            return View(dono);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                //procura o dono na BD
                Donos dono = db.Donos.Find(id);

            try
            {
               
                //marcar o 'dono' para a eliminação
                db.Donos.Remove(dono);
                //efetuar um 'commit' ao comando interior
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //cria uma mensagem de erro a ser apresentada ao utilizador
                ModelState.AddModelError("", string.Format("Ocorreu um erro na eliminação do Dono com ID={0}-{1}", id, dono.Nome));
                //invoca a View, com os dados do 'Dono' atual
                return View(dono);
                
            }
 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
