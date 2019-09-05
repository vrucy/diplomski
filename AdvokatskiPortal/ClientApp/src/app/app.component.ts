import { Component } from '@angular/core';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authService: AuthService){}
  _type: string;
  ngDoCheck(): void {
    if(this.authService.typeUserValue !== ''){
      this._type = localStorage.getItem('typeUser')
    }
  }
  ngOnInit(): void {
    
    this._type = localStorage.getItem('typeUser')
  }
  title = 'app';
}
