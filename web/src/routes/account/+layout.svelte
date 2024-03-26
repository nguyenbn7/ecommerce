<script>
	import logo from '$lib/assets/images/logo.png';
	import { APP_NAME } from '$lib/constant';
</script>

<div class="background-body">
	<div class="container">
		<div class="min-vh-100 d-flex flex-column align-items-center justify-content-center py-4">
			<div class="container">
				<div class="row justify-content-center">
					<div
						class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center">
						<div class="card mb-3 shadow rounded-3">
							<div class="d-flex justify-content-center pt-4">
								<a
									href="/"
									data-sveltekit-reload
									class="logo d-flex align-items-center w-auto text-decoration-none">
									<img src={logo} alt={APP_NAME} />
								</a>
							</div>
							<div class="card-body">
								<slot />
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<style lang="scss">
	.logo {
		line-height: 1;
	}

	@media (min-width: 1200px) {
		.logo {
			width: 280px;
		}
	}

	.logo img {
		max-height: 3em;
		margin-right: 6px;
	}

	.background-body {
		background-image: url($lib/assets/images/page-header.webp);
		background-repeat: no-repeat;
		background-attachment: fixed;
		background-position: 50% 75%;
	}
</style>
