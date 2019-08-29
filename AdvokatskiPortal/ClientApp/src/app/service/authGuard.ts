import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';


@Injectable()
export class AuthGuard implements CanActivate {

    // constructor(private auth: AuthService,private router: Router ) { }

    // canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    //     if(this.auth.isLogin){
    //         //navigate to home page
    //         //this.router.navigate(['/tabelaSaAdvokatima']);
    //         return true;
    //     }

    //     // not logged in so redirect to login page with the return url
    //     this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
    //     return false;
    // }
    constructor(private authService: AuthService, private route: Router) {}
    canActivate(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean  {
        if ( this.authService.isLogged()) {
          return true;
        } else {
          this.route.navigate(['login']);
          return false;
        }
    }
}
