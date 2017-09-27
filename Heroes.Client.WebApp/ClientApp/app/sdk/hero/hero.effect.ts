import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Effect, Actions } from "@ngrx/effects";
import { Action, Store } from "@ngrx/store";

import { AppState } from "../../core/app.state";
import { HERO_ACTION_TYPE, HeroAction } from "./hero.action";
import { HeroService } from "./hero.service";

@Injectable()
export class HeroEffect {

	@Effect() get$: Observable<Action> = this.actions$
		.ofType(HERO_ACTION_TYPE.get)
		.map(action => action.payload as string)
		.switchMap(key => this.service.get(key)
			.map(response => this.action.getSuccess(response))
			.catch(error => Observable.of(this.action.getFail(error))));

	constructor(
		private store: Store<AppState>,
		private actions$: Actions,
		private action: HeroAction,
		private service: HeroService
	) {
	}

}