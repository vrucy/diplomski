import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodavanjeDuplogAdvokataComponent } from './dodavanje-duplog-advokata.component';

describe('DodavanjeDuplogAdvokataComponent', () => {
  let component: DodavanjeDuplogAdvokataComponent;
  let fixture: ComponentFixture<DodavanjeDuplogAdvokataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodavanjeDuplogAdvokataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodavanjeDuplogAdvokataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
