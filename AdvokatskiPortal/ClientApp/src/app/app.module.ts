import { RegistrationCraftmanComponent } from './craftman/registration-craftman/registration-craftman.component';
import { FooterComponent } from './layout/footer/footer.component';
import { UserHeaderComponent } from './layout/user-header/user-header.component';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RoutingModule} from './routing.module';
import { HttpErrorInterceptor } from './service/http-error.interceptor';
import * as _moment from 'moment';
import { AuthGuard } from './service/authGuard';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { MaterialModule} from './material.module';
import { AuthInterceptor } from './service/authInterceptor';
import { AuthService } from './service/auth.service';
import { RegistarUserComponent } from './register/registar-user/registar-user.component';
import { TableCraftmansComponent } from './user/table-craftmans/table-craftmans.component';
import { CraftmanHeaderComponent } from './layout/craftman-header/craftman-header.component';
import { CreateCaseComponent } from './user/create-case/create-case.component';
import { SendCaseComponent } from './user/send-case/send-case.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReviewContractComponent } from './craftman/review-contract/review-contract.component';
import { AcceptComponent } from './craftman/dialog/accept/accept.component';
import { PreviewCaseComponent } from './user/preview-case/preview-case.component';
import { ModificationOfferComponent } from './craftman/dialog/modificartion-offer/modificartion-offer.component';
import { SuccessfullLoginComponent } from './snackBar/successfull-login/successfull-login.component';
import { PreviewCaseCraftmanComponent } from './craftman/dialog/preview-case-craftman/preview-case-craftman.component';
import { ErrorWriterComponent } from './snackBar/error-writer/error-writer.component';
import { AddCategoryComponent } from './craftman/add-category/add-category.component';
import { AddSubCategoryComponent } from './craftman/add-sub-category/add-sub-category.component';
import { NotificationComponent } from './layout/notification/notification.component';
import { MatSortModule } from '@angular/material/sort';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { EditCraftmanComponent } from './craftman/edit-craftman/edit-craftman.component';
import { ShowPicturesComponent } from './user/dialog/show-pictures/show-pictures.component';
import { EditCaseComponent } from './user/edit-case/edit-case.component';
import { SuccessfullCreateCaseComponent } from './snackBar/successfull-create-case/successfull-create-case.component';
import { SuccessfullRegistrationComponent } from './snackBar/successfull-registration/successfull-registration.component';
import { SuccessfullSendCaseComponent } from './snackBar/successfull-send-case/successfull-send-case.component';
import { ErrorPageComponent } from './layout/error-page/error-page.component';
// import { PdfViewerModule } from 'ng2-pdf-viewer';
// import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RegistarUserComponent,
    UserHeaderComponent,
    FooterComponent,
    TableCraftmansComponent,
    RegistrationCraftmanComponent,
    CraftmanHeaderComponent,
    CreateCaseComponent,
    SendCaseComponent,
    PreviewCaseComponent,
    ReviewContractComponent,
    AcceptComponent,
    ModificationOfferComponent,
    SuccessfullLoginComponent,
    PreviewCaseCraftmanComponent,
    ErrorWriterComponent,
    AddCategoryComponent,
    AddSubCategoryComponent,
    NotificationComponent,
    EditUserComponent,
    EditCraftmanComponent,
    ShowPicturesComponent,
    SuccessfullCreateCaseComponent,
    EditCaseComponent,
    CreateCaseComponent,
    SuccessfullRegistrationComponent,
    SuccessfullSendCaseComponent,
    ErrorPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MaterialModule,
    FormsModule,
    RouterModule,
    RoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatSortModule

  ], entryComponents: [AcceptComponent, ModificationOfferComponent , SuccessfullLoginComponent, 
    ErrorWriterComponent, ShowPicturesComponent, PreviewCaseComponent, SuccessfullCreateCaseComponent, PreviewCaseCraftmanComponent,
                      CreateCaseComponent,SuccessfullRegistrationComponent, SuccessfullSendCaseComponent],
    providers: [AuthGuard, AuthService , {
    provide : HTTP_INTERCEPTORS,
    useClass : AuthInterceptor,
    multi : true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true
  }],
  exports: [ MatSortModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
