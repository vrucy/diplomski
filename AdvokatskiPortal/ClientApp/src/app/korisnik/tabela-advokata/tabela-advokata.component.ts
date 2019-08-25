import { Advokat } from './../../model/Advokat';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { KorisnikService } from '../../service/korisnik.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-tabela-advokata',
  templateUrl: './tabela-advokata.component.html',
  styleUrls: ['./tabela-advokata.component.css']
})
export class TabelaAdvokataComponent implements OnInit {
  displayedColumns: string[] = ['select', 'Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  advokati;
  slucajeviKorisnika;
  slucaj;
  selection = new SelectionModel<Advokat>(true, []);
  public dataSource = new MatTableDataSource<Advokat>();

  constructor(private korsinikService: KorisnikService) { }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: Advokat): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }
  ngOnInit() {
    this.korsinikService.getAllSlucajForKorisnik().subscribe(res =>{
      this.slucajeviKorisnika = res;
      console.log(res)
    });
    this.korsinikService.getAllAdvokati().subscribe(res => {
      this.dataSource.data = res;
      this.advokati = res;
      console.log(res);
    });
  }
  SendChosenAdvokat() {
    console.log(this.slucaj.id)
   // console.log(this.selection.selected);
   const x = { Slucaj: this.slucaj, Advokats: this.selection.selected }
    this.korsinikService.postSlucajAdvokatima(x);
    
  }
}
