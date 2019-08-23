import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaAdvokataComponent } from './tabela-advokata.component';

describe('TabelaAdvokataComponent', () => {
  let component: TabelaAdvokataComponent;
  let fixture: ComponentFixture<TabelaAdvokataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabelaAdvokataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabelaAdvokataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
