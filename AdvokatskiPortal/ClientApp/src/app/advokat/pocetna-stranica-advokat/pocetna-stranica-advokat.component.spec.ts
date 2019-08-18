import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PocetnaStranicaAdvokatComponent } from './pocetna-stranica-advokat.component';

describe('PocetnaStranicaAdvokatComponent', () => {
  let component: PocetnaStranicaAdvokatComponent;
  let fixture: ComponentFixture<PocetnaStranicaAdvokatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PocetnaStranicaAdvokatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PocetnaStranicaAdvokatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
