using DotJwt.Model;
using Microsoft.AspNetCore.Mvc;

namespace DotJwt.Interface {
    public interface UserAuth {

        public Task<ActionResult> Login(UserModel? userModel);

    }
}
