import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PocetnaStranicaKorisnikComponent } from './korisnik/pocetna-stranica-korisnik/pocetna-stranica-korisnik.component';
import { PocetnaStranicaAdvokatComponent } from './advokat/pocetna-stranica-advokat/pocetna-stranica-advokat.component';


const router: Routes = [
  { 
    path: 'login', component: LoginComponent
  },
  {
    path: 'pocetnaAdvokat', component: PocetnaStranicaAdvokatComponent 
  },
  {
    path: 'pocetnaKorisnik', component: PocetnaStranicaKorisnikComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(router)
  ],
  declarations: []
})
export class RoutingModule { }
