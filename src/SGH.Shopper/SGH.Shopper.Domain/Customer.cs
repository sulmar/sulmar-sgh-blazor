using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGH.Shopper.Domain;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }  
    public override bool HasContent(string content) => FirstName.Contains(content) || LastName.Contains(content) || Email.Contains(content);
    
}
