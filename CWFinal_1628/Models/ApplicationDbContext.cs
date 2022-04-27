using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CWFinal_1628.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MaterialManagemntConnect", throwIfV1Schema: false)
        {   
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Supervisor> Supervisors { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Builder> Builders { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.MaterialType> MaterialTypes { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Material> Materials { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Investor> Investors { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Salesman> Salesmen { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.ProjectMaterial> ProjectMaterials { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Work> Works { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.AssignProject> AssignProjects { get; set; }

        public System.Data.Entity.DbSet<CWFinal_1628.Models.Invoice> Invoices { get; set; }
    }
}