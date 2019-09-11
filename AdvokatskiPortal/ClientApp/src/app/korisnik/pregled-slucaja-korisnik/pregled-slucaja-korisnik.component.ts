import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-pregled-slucaja-korisnik',
  templateUrl: './pregled-slucaja-korisnik.component.html',
  styleUrls: ['./pregled-slucaja-korisnik.component.css']
})
export class PregledSlucajaKorisnikComponent implements OnInit {

  displayedColumns: string[] = ['ime', 'prezime', 'vrstaPlacanja', 'cena', 'opis', 'button'];
  public dataSource = [];
  private data: any;
  public isLoadead = false;

  constructor(private korisnikService: KorisnikService) { }

  ngOnInit() {
    this.korisnikService.GetUgovorsForKorisnik().subscribe(res => {
      this.data = res;
      this.dataSource = res;
      console.log(this.dataSource);
      this.isLoadead = true;
    });
  }

  test(tabChangeEvent): void {
    // if (tabChangeEvent.index === 0) {
      this.isLoadead = false;
      setTimeout(() => {
        const copy: any[] = [...this.data];
        const tmpData = copy.filter(c => c.slucajStatusId === ++tabChangeEvent.index);
        this.dataSource = tmpData;
        console.log(this.dataSource);
        this.isLoadead = true;
      }, 100);
    }
}
