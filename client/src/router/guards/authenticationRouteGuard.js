import store from './../../store';

export const authenticationRouteGuard = (to, from, next) => {
	if (!store.state.user) {
		next();
	}
	else {
		next(from.path);
	}
}  