using GestoreRimborsi.Core.Entities;
using GestoreRimborsi.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreRimborsi.Core.BusinessLayer
{
    public class BusinessLayer: IBusinessLayer
    {
        private readonly IRepositoryMonitoraggioSpese speseRep;
        private readonly IRepositoryDipendenti dipendentiRep;


        public BusinessLayer(IRepositoryMonitoraggioSpese monitoraggioSpeseRepository, IRepositoryDipendenti dipendentiRepository)
        {
            speseRep = monitoraggioSpeseRepository;
            dipendentiRep = dipendentiRepository;
        }

        public void Update()
        {
            try
            {
                List<MonitoraggioSpesa> monitoraggi = new List<MonitoraggioSpesa>();

                monitoraggi = speseRep.GetItemsWithOutApproval();
                foreach (var monitoraggio in monitoraggi)
                {
                    if (monitoraggio.Spesa <= 400)
                    {
                        monitoraggio.Approvata = true;
                        monitoraggio.Approvatore = ApprovatoreEnum.Manager;
                        monitoraggio.Rimborso = CalcolaRimborso(monitoraggio);
                        speseRep.Update(monitoraggio);

                    }
                    else if (monitoraggio.Spesa > 400 && monitoraggio.Spesa <= 1000)
                    {
                        monitoraggio.Approvata = true;
                        monitoraggio.Approvatore = ApprovatoreEnum.OperationManager;
                        monitoraggio.Rimborso = CalcolaRimborso(monitoraggio);
                        speseRep.Update(monitoraggio);
                    }
                    else if (monitoraggio.Spesa > 1000 && monitoraggio.Spesa <= 2500)
                    {
                        monitoraggio.Approvata = true;
                        monitoraggio.Approvatore = ApprovatoreEnum.CEO;
                        monitoraggio.Rimborso = CalcolaRimborso(monitoraggio);
                        speseRep.Update(monitoraggio);
                    }
                    else if (monitoraggio.Spesa > 2500)
                    {
                        monitoraggio.Approvata = false;
                        monitoraggio.Rimborso = 0;
                        speseRep.Update(monitoraggio);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public double? CalcolaRimborso(MonitoraggioSpesa monitoraggio)
        {
            try
            {


                if (monitoraggio.Categoria == CategoriaEnum.Vitto)
                {
                    monitoraggio.Rimborso = ((monitoraggio.Spesa) / 100) * 70;

                }
                else if (monitoraggio.Categoria == CategoriaEnum.Alloggio)
                {
                    monitoraggio.Rimborso = monitoraggio.Spesa;

                }
                else if (monitoraggio.Categoria == CategoriaEnum.Trasferta)
                {
                    monitoraggio.Rimborso = ((monitoraggio.Spesa / 2) + 50);

                }
                else if (monitoraggio.Categoria == CategoriaEnum.Altro)
                {
                    monitoraggio.Rimborso = ((monitoraggio.Spesa / 100) * 10);

                }

                return monitoraggio.Rimborso;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ScriviSuFile(Evento evento)
        {
            try
            {
                List<Dipendente> dipendenti = new List<Dipendente>();
                dipendenti = dipendentiRep.FetchDipendenti();


                string path = @"C:\Users\annamaria.ciasca\source\repos\GestoreRimborsi\Riepilogo.txt";

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    Console.WriteLine("Inserisci il nome del dipendente di cui vuoi stampare il riepilogo delle spese:");
                    string nome = Console.ReadLine();

                    if ((dipendenti.Any(d => d.Nome == nome)))
                    {

                        List<MonitoraggioSpesa> monitoraggi = speseRep.GetByName(nome);
                        sw.WriteLine($"Spese di {nome}: ");
                        foreach (var m in monitoraggi)
                        {
                            sw.WriteLine($"Data: {m.Data}  -  Categoria: {m.Categoria} " +
                                $" -  Spesa sostenuta: {m.Spesa}€  -  Approvata: {m.Approvata}  -  Rimborso: {m.Rimborso}€");
                        }
                        sw.WriteLine("\n");

                        Console.WriteLine("Riepilogo stampato correttamente");
                    }
                    else
                    {

                        Console.WriteLine("Il Nome inserito non è presente nel DB");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        }

    }

