import { RegistrationCraftmanComponent } from './majstor/registration-craftman/registration-craftman.component';
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
import { ReviewContractComponent } from './majstor/review-contract/review-contract.component';
import { AcceptComponent } from './majstor/dialog/accept/accept.component';
import { PreviewCaseComponent } from './user/preview-case/preview-case.component';
import { ModificationOfferComponent } from './majstor/dialog/modificartion-offer/modificartion-offer.component';
import { SuccessfullLoginComponent } from './snackBar/successfull-login/successfull-login.component';
import { FaliedLoginComponent } from './snackBar/failed-login/failed-login.component';
import { PreviewCaseCraftmanComponent } from './majstor/dialog/preview-case-craftman/preview-case-craftman.component';
import { AddDoubleCraftmanComponent } from './snackBar/add-double-craftman/add-double-craftman.component';
import { AddCategoryComponent } from './majstor/add-category/add-category.component';
import { AddSubCategoryComponent } from './majstor/add-sub-category/add-sub-category.component';
import { PreviewCaseUserComponent } from './user/dialog/preview-case-user/preview-case-user.component';
import { NotificationComponent } from './layout/notification/notification.component';
import { MatSortModule } from '@angular/material/sort';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { EditCraftmanComponent } from './majstor/edit-craftman/edit-craftman.component';
import { ShowPicturesComponent } from './user/dialog/show-pictures/show-pictures.component';
import { EditCaseComponent } from './user/edit-case/edit-case.component';
import { SuccessfullCreateCaseComponent } from './snackBar/successfull-create-case/successfull-create-case.component';
import { SuccessfullRegistrationComponent } from './snackBar/successfull-registration/successfull-registration.component';
import { SuccessfullSendCaseComponent } from './snackBar/successfull-send-case/successfull-send-case.component';
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
    PreviewCaseUserComponent,
    ModificationOfferComponent,
    SuccessfullLoginComponent,
    FaliedLoginComponent,
    PreviewCaseCraftmanComponent,
    AddDoubleCraftmanComponent,
    AddCategoryComponent,
    AddSubCategoryComponent,
    PreviewCaseUserComponent,
    NotificationComponent,
    EditUserComponent,
    EditCraftmanComponent,
    ShowPicturesComponent,
    SuccessfullCreateCaseComponent,
    EditCaseComponent,
    CreateCaseComponent,
    SuccessfullRegistrationComponent,
    SuccessfullSendCaseComponent
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
    // PdfViewerModule,
    // NgxExtendedPdfViewerModule

  ], entryComponents: [AcceptComponent, ModificationOfferComponent , SuccessfullLoginComponent, FaliedLoginComponent, PreviewCaseUserComponent,
                       AddDoubleCraftmanComponent, ShowPicturesComponent, PreviewCaseComponent, SuccessfullCreateCaseComponent, PreviewCaseCraftmanComponent,
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
