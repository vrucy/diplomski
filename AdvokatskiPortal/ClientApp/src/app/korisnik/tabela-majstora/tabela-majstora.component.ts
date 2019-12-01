import { Majstor } from '../../model/Majstor';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { KorisnikService } from '../../service/korisnik.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-tabela-majstora',
  templateUrl: './tabela-majstora.component.html',
  styleUrls: ['./tabela-majstora.component.css']
})
export class TabelaMajstoraComponent implements OnInit {
  displayedColumns: string[] = ['select', 'Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email'];
  majstori;
  slucajeviKorisnika;
  slucaj;
  selection = new SelectionModel<Majstor>(true, []);
  public dataSource = new MatTableDataSource<Majstor>();

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

  checkboxLabel(row?: Majstor): string {
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
    this.korsinikService.getAllMajstori().subscribe((res: any) => {
      this.dataSource.data = res;
      this.majstori = res;
      console.log(res);
    });
  }
  SendChosenMajstor() {
    console.log(this.slucaj.id)
   // console.log(this.selection.selected);
   const x = { Slucaj: this.slucaj, Majstors: this.selection.selected }
    this.korsinikService.postSlucajMajstorima(x);

  }
}
