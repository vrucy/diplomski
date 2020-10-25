import { Component, OnInit } from '@angular/core';
import { User } from '../../model/User';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  currentUser:User;

  constructor( private authService: AuthService) {
  
   }
 
   ngOnInit() {
    this.authService.getUser().subscribe( (res: any) => {
      this.currentUser = res;
      this.currentUser.FirstName = res.FirstName;
    });
    
     }
    Submit() {
    this.authService.editProfilUser(this.currentUser);
    }

}
