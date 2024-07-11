import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MatDialogRef} from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

import { Articulo } from '../../../Interfaces/articulo';
import { Tienda } from '../../../Interfaces/tienda';

import { ArticuloService } from '../../../Services/articulo.service';
import { TiendaService } from '../../../Services/tienda.service';

export const DATE_FORMATS = {
  parse: {
    dateInput: 'dd/MM/yyyy'
  },
  display: {
    dateInput: 'dd/MM/yyyy',
    monthYearLabel: 'MMMM yyyy',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM yyyy'
  }
}

@Component({
  selector: 'dialog-articulo-tienda',
  templateUrl: './articulo-tienda.component.html',
  styleUrl: './articulo-tienda.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{provide: MAT_DATE_FORMATS, useValue: DATE_FORMATS }]
})
export class DialogArticuloTiendaComponent {
  formArticuloTienda: FormGroup;
  articuloControl = new FormControl<Articulo | null>(null, Validators.required);
  tiendaControl = new FormControl<Tienda | null>(null, Validators.required);
  articulos: Articulo[] = [];
  tiendas: Tienda[] = [];
  actionTitle: string = "Nuevo";
  actionButton: string = "Guardar";

  constructor(
    private dialogRef: MatDialogRef<DialogArticuloTiendaComponent>,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private articuloService: ArticuloService,
    private tiendaService: TiendaService
  ) {
    this.formArticuloTienda = this.formBuilder.group({
      idArticulo: ['', Validators.required],
      articulo: ['', Validators.required],
      idTienda: ['', Validators.required],
      tienda: ['', Validators.required],
      fecha: ['']
    });
    this.tiendaService.getTiendas().subscribe(
      (data: Tienda[]) => {
        this.tiendas = data;
      }
    );
    this.articuloService.getArticulos().subscribe(
      (data: Articulo[]) => {
        this.articulos = data;
      }
    );
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000
    });
  }

  executeAction() {
    console.log(this.formArticuloTienda);
    console.log(this.formArticuloTienda.value);
  }
}
