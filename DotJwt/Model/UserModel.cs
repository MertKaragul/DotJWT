using System.ComponentModel.DataAnnotations;

namespace DotJwt.Model {
    public class UserModel {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public String Roles { get; set; } = "USER";

    }
}
