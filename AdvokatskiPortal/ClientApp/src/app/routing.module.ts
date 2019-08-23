import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PocetnaStranicaKorisnikComponent } from './korisnik/pocetna-stranica-korisnik/pocetna-stranica-korisnik.component';
import { PocetnaStranicaAdvokatComponent } from './advokat/pocetna-stranica-advokat/pocetna-stranica-advokat.component';
import { RegistarKorisnikComponent } from './register/registar-korisnik/registar-korisnik.component';
import { RegistracijaAdvokataComponent } from './advokat/registracija-advokata/registracija-advokata.component';


const router: Routes = [
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'registration', component: RegistarKorisnikComponent
 },
 {
    path: 'registracijaAdvokata', component: RegistracijaAdvokataComponent
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
