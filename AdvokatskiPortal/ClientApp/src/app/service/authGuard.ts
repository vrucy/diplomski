import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';


@Injectable()
export class AuthGuard implements CanActivate {

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
