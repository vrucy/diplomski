import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatTabChangeEvent } from '@angular/material';

@Component({
  selector: 'app-pregled-ugovora',
  templateUrl: './pregled-ugovora.component.html',
  styleUrls: ['./pregled-ugovora.component.css']
})
export class PregledUgovoraComponent implements OnInit {
  displayedColumns: string[] = ['ime','prezime', 'cena', 'opis', 'button'];
  public dataSource = new MatTableDataSource();

  constructor(private advokatService: AdvokatService) { }

  ngOnInit() {
    this.advokatService.getUgovorsForAdvokat().subscribe(res => {
      this.dataSource = res;
      console.log(res);
    });
  }
  test = (tabChangeEvent): void => {
    if(tabChangeEvent.index == 0){
      console.log("prvi tab");
    } else if (tabChangeEvent.index == 1) {
      this.advokatService.getSlucajiPrihvaceni().subscribe(res => {
        console.log(res);
      })
    }else{
      console.log('treci tab');
    }

  }
}
