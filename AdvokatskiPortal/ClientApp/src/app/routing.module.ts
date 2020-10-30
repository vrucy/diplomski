import { AddSubCategoryComponent } from './craftman/add-sub-category/add-sub-category.component';
import { PreviewCaseComponent } from './user/preview-case/preview-case.component';
import { AuthGuard } from './service/authGuard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistarUserComponent } from './register/registar-user/registar-user.component';
import { RegistrationCraftmanComponent } from './craftman/registration-craftman/registration-craftman.component';
import { TableCraftmansComponent } from './user/table-craftmans/table-craftmans.component';
import { CreateCaseComponent } from './user/create-case/create-case.component';
import { SendCaseComponent } from './user/send-case/send-case.component';
import { ReviewContractComponent } from './craftman/review-contract/review-contract.component';
//import { PrihvacenOdgovorComponent } from './majstor/odgovoriNaPonude/prihvacen-odgovor/prihvacen-odgovor.component';
import { AddCategoryComponent } from './craftman/add-category/add-category.component';
import { NotificationComponent } from './layout/notification/notification.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { EditCraftmanComponent } from './craftman/edit-craftman/edit-craftman.component';
import { EditCaseComponent } from './user/edit-case/edit-case.component';

const router: Routes = [
  {
    path: 'Login', component: LoginComponent
  },
  {
    path: 'Registration', component: RegistarUserComponent
 },
 {
    path: 'RegistrationCraftman', component: RegistrationCraftmanComponent
 },
  {
    path: 'TableCraftmans', component: TableCraftmansComponent, canActivate: [AuthGuard]
  },
  {
    path: 'CreateCase', component: CreateCaseComponent, canActivate: [AuthGuard]
  },
  {
    path: 'SendCase', component: SendCaseComponent, canActivate: [AuthGuard]
  },
  {
     path: '', redirectTo: '/Login', pathMatch: 'full'
  },
  {
    path: 'ReviewContract', component: ReviewContractComponent
  },
  {
    path: 'PreviewCaseUser', component: PreviewCaseComponent
  },
  {
    path: 'EditUser', component: EditUserComponent
  },
  {
    path: 'EditCraftman', component: EditCraftmanComponent
  },
  {
    path: 'EditCase/:id', component: EditCaseComponent, canActivate: [AuthGuard]
  },
  {
    path: 'AddCategory', component: AddCategoryComponent
  },
  {
    path: 'AddSubCategory', component: AddSubCategoryComponent
  },
  {
    path: 'Notification' , component: NotificationComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(router)
  ],
  declarations: []
})
export class RoutingModule { }
