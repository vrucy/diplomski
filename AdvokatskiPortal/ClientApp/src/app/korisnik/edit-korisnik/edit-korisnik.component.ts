import { Component, OnInit } from '@angular/core';
import { Korisnik } from '../../model/Korisnik';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-edit-korisnik',
  templateUrl: './edit-korisnik.component.html',
  styleUrls: ['./edit-korisnik.component.css']
})
export class EditKorisnikComponent implements OnInit {
  trenutniKorisnik;

  constructor( private authService: AuthService) {
  
   }
 
   ngOnInit() {
    this.authService.getKorisnik().subscribe( (res: any) => {
      this.trenutniKorisnik = res;
      console.log(this.trenutniKorisnik)
    });
    
     }
    Submit() {
    this.authService.editProfilKorisnik(this.trenutniKorisnik);
    }

}
