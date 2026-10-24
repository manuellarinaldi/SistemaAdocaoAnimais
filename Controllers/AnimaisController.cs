
using Microsoft.AspNetCore.Mvc;
using SistemaAdocaoAnimais.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaAdocaoAnimais.Controllers
{
    public class AnimaisController : Controller
    {

        private void CarregarOpcoes()
        {
            ViewBag.Especies = new List<string> { "Cachorro", "Gato", "Coelho", "Outro" };
            ViewBag.Portes = new List<string> { "Pequeno", "Médio", "Grande" };
        }
        private static List<Animal> animais = new List<Animal>();
        private static int proximoId = 1;

        public IActionResult Index(string? pesquisa)
        {
            var resultado = animais.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                resultado = resultado.Where(a =>
                    a.Nome.Contains(pesquisa,
                        System.StringComparison.OrdinalIgnoreCase)
                    || a.Especie.Contains(pesquisa,
                        System.StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.Pesquisa = pesquisa;

            return View(resultado.ToList());
        }

        public IActionResult Create()
        {
            CarregarOpcoes();          

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                CarregarOpcoes();          

                return View(animal);
            }

            animal.Id = proximoId++;
            animais.Add(animal);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            
            var animal = animais.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound();

            CarregarOpcoes();          
            return View(animal);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Animal animal)
        {
            if (id != animal.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                CarregarOpcoes();      
                return View(animal);
            }

            var existente = animais.FirstOrDefault(a => a.Id == id);

            if (existente == null)
            {
                return NotFound();
            }

            existente.Nome = animal.Nome;
            existente.Especie = animal.Especie;
            existente.Raca = animal.Raca;
            existente.Idade = animal.Idade;
            existente.DataEntrada = animal.DataEntrada;
            existente.Porte = animal.Porte;
            existente.Status = animal.Status;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var animal = animais.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var animal = animais.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            animais.Remove(animal);

            return RedirectToAction(nameof(Index));
        }
    }
}
