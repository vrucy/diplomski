import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-edit-craftman',
  templateUrl: './edit-craftman.component.html',
  styleUrls: ['./edit-craftman.component.css']
})
export class EditCraftmanComponent implements OnInit {
  currentUser;

  constructor( private authService: AuthService) {
  
   }
 
   ngOnInit() {
    this.authService.getCraftman().subscribe( (res: any) => {
      this.currentUser = res;
    });
    
     }
    Submit() {
      this.authService.editProfilCraftman(this.currentUser);
    }

}
