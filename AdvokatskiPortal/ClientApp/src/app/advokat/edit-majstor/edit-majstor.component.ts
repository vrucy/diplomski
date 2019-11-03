import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-edit-majstor',
  templateUrl: './edit-majstor.component.html',
  styleUrls: ['./edit-majstor.component.css']
})
export class EditMajstorComponent implements OnInit {
  trenutniKorisnik;

  constructor( private authService: AuthService) {
  
   }
 
   ngOnInit() {
    this.authService.getMajstor().subscribe( (res: any) => {
      this.trenutniKorisnik = res;
      console.log(this.trenutniKorisnik)
    });
    
     }
    Submit() {
    this.authService.editProfilMajstor(this.trenutniKorisnik);
    }

}
