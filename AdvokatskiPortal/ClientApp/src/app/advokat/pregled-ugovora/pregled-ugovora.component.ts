import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pregled-ugovora',
  templateUrl: './pregled-ugovora.component.html',
  styleUrls: ['./pregled-ugovora.component.css']
})
export class PregledUgovoraComponent implements OnInit {
  displayedColumns: string[] = ['ime', 'prezime', 'tekst_Zahteva', 'cena', 'button'];

  constructor(private advokatService: AdvokatService) { }

  ngOnInit() {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      console.log(res);
    });
  }

}
