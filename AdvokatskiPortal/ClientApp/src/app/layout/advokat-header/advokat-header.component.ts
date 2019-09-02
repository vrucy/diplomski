import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-advokat-header',
  templateUrl: './advokat-header.component.html',
  styleUrls: ['./advokat-header.component.css']
})
export class AdvokatHeaderComponent implements OnInit {
  badgeCount;
  constructor(private advokatService: AdvokatService) { }

  ngOnInit() {
    this.advokatService.getNewNostifiation().subscribe( res => {
      console.log(res);
      this.badgeCount = res;
    });
  }
  clearCount() {
    this.badgeCount = 0;
  }
}
