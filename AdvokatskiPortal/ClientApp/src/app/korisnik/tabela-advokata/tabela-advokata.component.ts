import { Advokat } from './../../model/Advokat';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-tabela-advokata',
  templateUrl: './tabela-advokata.component.html',
  styleUrls: ['./tabela-advokata.component.css']
})
export class TabelaAdvokataComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Ime', 'Prezime', 'Mesto', 'Ulica', 'Email', 'UserName'];

  public dataSource = new MatTableDataSource<Advokat>();

  constructor() { }

  ngOnInit() {
  }

}
