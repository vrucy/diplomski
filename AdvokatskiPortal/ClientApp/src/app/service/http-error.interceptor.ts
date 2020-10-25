import { FaliedLoginComponent } from './../snackBar/failed-login/failed-login.component';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';
import { AddDoubleCraftmanComponent } from '../snackBar/add-double-craftman/add-double-craftman.component';

export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(private _snackBar: MatSnackBar) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(1),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
          } else {
            // server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          if (error.status === 405 ) {
            this._snackBar.openFromComponent(FaliedLoginComponent, {
              duration: 3000
            });
          }
          if ( error.status === 404) {
            console.log(error);
            this._snackBar.openFromComponent(AddDoubleCraftmanComponent, {
              data: error.error.message,
              duration: 3000
            })
          }
          return throwError(errorMessage);
        })
      );
  }
}
