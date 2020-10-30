import { Injectable  } from '@angular/core'
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http'
import { Observable  } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { of } from 'rxjs/internal/observable/of';
@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    constructor() { }
    intercept (req , next){
        var token = localStorage.getItem('token')
        var authRequest = req.clone({
            headers : req.headers.set('Authorization' , `Bearer ${token}` )
        })
        return next.handle(authRequest);
    }
    // intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //     request = request.clone({
    //       setHeaders: {
    //         Authorization: `Bearer ${localStorage.getItem('token')}`,
    //         'Content-Type': 'application/json'
    //       }
    //     });
    //     return next.handle(request).pipe(
    //       catchError(
    //         (err, caught) => {
    //             console.log(err.status)
    //           if (err.status === 401){
    //             this.handleAuthError();
    //             return of(err);
    //           }
    //           throw err;
    //         }
    //       )
    //     );
    //   }
    //   private handleAuthError() {
    //     localStorage.delete('token');
    //     console.log('na pocetnu stranu')
    //     //this.router.navigateByUrl('signIn');
    //   }
}
