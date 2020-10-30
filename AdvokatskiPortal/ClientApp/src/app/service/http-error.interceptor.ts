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
import { ErrorWriterComponent } from '../snackBar/error-writer/error-writer.component';

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
          
          if (error.url.endsWith("PostCaseCraftmanima") || error.url.endsWith("Login")){
            this._snackBar.openFromComponent(ErrorWriterComponent, {
              data: error.error.message,
              duration: 3000
            });
          }
          if (error.status === 401){
            this.handleAuthError();
          }

          return throwError(errorMessage);
        })
      );
  }
  private handleAuthError() {
    localStorage.clear();
    console.log('na pocetnu stranu')
    //this.router.navigateByUrl('signIn');
  }
}
