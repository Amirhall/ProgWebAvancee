using labo.signalr.api.Data;
using labo.signalr.api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace labo.signalr.api.Hubs
{
    public class TaskHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public TaskHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public async override Task OnConnectedAsync( )
        {
            base.OnConnectedAsync();
            IEnumerable<UselessTask> tasklistServer = await GetAllTasks();
            await Clients.Client(Context.ConnectionId).SendAsync("tasklist", tasklistServer);
        }


        public async Task RecevoirUneNouvelleTache(string tache)
        {

            _context.UselessTasks.Add(new Models.UselessTask()
            {
                Text = tache,
                Completed = false
            });
            await _context.SaveChangesAsync();

            IEnumerable<UselessTask> tasklistServer = await GetAllTasks();
            await Clients.All.SendAsync("tasklist", tasklistServer);
        }

        public async Task ChangerCompletion(int id)
        {
            UselessTask task = await _context.UselessTasks.FindAsync(id);
            if (task == null)
            {
                return;
            }


            task.Completed = !task.Completed;
            await _context.SaveChangesAsync();

            IEnumerable<UselessTask> tasklistServer = await GetAllTasks();
            await Clients.All.SendAsync("tasklist", tasklistServer);
        }

        // Méthode interne pour retourner uniquement la liste des tâches
        private async Task<IEnumerable<UselessTask>> GetAllTasks()
        {
            return await _context.UselessTasks.ToListAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            base.OnDisconnectedAsync(exception);
            // TODO: Ajouter votre logique
        }

    }
}
