import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { FormControl } from '@angular/forms';
import { SlucajSlanjeVM } from '../../model/SlucajSlanjeVM';

@Component({
  selector: 'app-pregled-slucaja-korisnik',
  templateUrl: './pregled-slucaja-korisnik.component.html',
  styleUrls: ['./pregled-slucaja-korisnik.component.css']
})
export class PregledSlucajaKorisnikComponent implements OnInit {

  displayedColumns: string[] = ['ime', 'prezime', 'vrstaPlacanja', 'cena', 'opis', 'button'];
  public dataSource = new MatTableDataSource<SlucajSlanjeVM>();

  nameFilter = new FormControl('');
  tabIndex = new FormControl('');

  constructor(private korisnikService: KorisnikService) {
    this.korisnikService.GetAllSlucajAdvokatForKorisnik().subscribe(res => {
      this.dataSource.data = res;
      console.log(res)
    });
    this.dataSource.filterPredicate = this.tableFilter();
  }


  filterValues = {
    name: '',
    tabIndex: ''
  };

  ngOnInit() {
    this.nameFilter.valueChanges
      .subscribe(
        name => {
          this.filterValues.name = name;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
    this.tabIndex.valueChanges
      .subscribe(
        id => {
          this.filterValues.tabIndex = id;
          this.dataSource.filter = JSON.stringify(this.filterValues);
        }
      );
  }
  tableFilter(): (data: any, filter: string) => boolean {
    let filterFunction = function (data, filter): boolean {
      let searchTerms = JSON.parse(filter);
      return data.advokat.ime.toLowerCase().indexOf(searchTerms.name) !== -1 ||
        data.slucajStatusId.toString().toLowerCase().indexOf(searchTerms.tabIndex) !== -1;
    }
    return filterFunction;
  }

  tabDirect(event) {
    if (event.index === 0) {
      this.resetFilter();
      
      this.tabIndex.setValue(1);
      console.log('set 1');
    } else if (event.index === 1) {
      this.resetFilter();
      this.tabIndex.setValue(2);
      
    } else if (event.index === 2) {
      this.resetFilter();
      this.tabIndex.setValue(6);
      console.log('set 3');
    } else if (event.index === 3){
      this.resetFilter();
      this.tabIndex.setValue(4);
      console.log('set 4');
    } 
  }
  chekerTab(event) {
    const even = ++event.index;
    this.tabIndex.setValue(even);
    this.resetFilter();
  }
  resetFilter() {
    this.nameFilter.reset();
    this.tabIndex.reset();
  }
  prihvacenSlucaj(slucaj) {
    this.korisnikService.prihvacenSlucajOdKorisnika(slucaj);
  }
  odbijenSlucaj(slucaj) {
    this.korisnikService.odbijenSlucajOdKorisnika(slucaj);
  }
}
