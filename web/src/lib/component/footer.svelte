<script>
	import paymentsImage from '$lib/assets/images/payments.png';

	const usefulPages = [
		{ href: '/shop', name: 'Shop' },
		{ href: '/basket', name: 'Basket' },
		{ href: '/about', name: 'About Us' },
		{ href: '/blog', name: 'News' },
		{ href: '/career', name: 'Career' }
	];
</script>

<footer class="bg-light-subtle">
	<div class="container-fluid px-5">
		<div class="px-3 pt-5">
			<div class="row">
				<div class="col-md-4">
					<h5 class="mb-4">Contact Us</h5>
					<ul class="nav flex-column">
						<li class="nav-item mb-2">
							<a
								href="http://maps.google.com/?q=199 Avenue S, Lubbock, TX 79415, USA"
								class="nav-link p-0 text-body-secondary">
								<i class="fa-solid fa-location-dot me-1"></i> 199 Avenue S, Lubbock, Texas 79415, USA
							</a>
						</li>

						<li class="nav-item mb-2">
							<a href="mailto:info@example.com" class="nav-link p-0 text-body-secondary">
								<i class="fa-solid fa-envelope me-1"></i> info@example.com
							</a>
						</li>

						<li class="nav-item mb-2">
							<a href="tel:+1999999999" class="nav-link p-0 text-body-secondary">
								<i class="fa-solid fa-phone me-1"></i> (999) 999-9999
							</a>
						</li>
					</ul>
				</div>
				<div class="col-md-8">
					<div class="row">
						<div class="col-3 col-md-2 mb-3">
							<h5>Useful pages</h5>
							<ul class="nav flex-column">
								{#each usefulPages as usefulPage}
									<li class="nav-item mb-2">
										<a
											href={usefulPage.href}
											class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary">
											{usefulPage.name}
										</a>
									</li>
								{/each}
							</ul>
						</div>

						<div class="col-9 col-md-4 ps-5 mb-3">
							<h5>Support</h5>
							<ul class="nav flex-column">
								<li class="nav-item mb-2">
									<a
										href="/contact"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary">
										Contact Us
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href="/faq"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary">
										Shipping & Returns
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href={'#'}
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary">
										Tracking Order
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href={'#'}
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary">
										Frequently Asked Questions
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href={'#'}
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary">
										Privacy Policy
									</a>
								</li>
							</ul>
						</div>

						<div class="col-md-5 ms-2 flex-grow-1">
							<form>
								<h5>Subscribe to our newsletter</h5>
								<p>Monthly digest of what's new and exciting from us.</p>
								<div class="d-flex flex-column flex-sm-row w-100 gap-2">
									<label for="newsletter1" class="visually-hidden">Email address</label>
									<input
										id="newsletter1"
										type="text"
										class="form-control"
										placeholder="Email address" />
									<button class="btn btn-primary" type="button">Subscribe</button>
								</div>
							</form>
						</div>
					</div>
				</div>
			</div>

			<div class="row pt-4 pb-4 mt-5 border-top">
				<p class="col-12 col-md-4 text-center text-md-start">
					© 2023 Skinet. All rights reserved.
				</p>
				<div class="col-12 col-md-4 text-center">
					<div class="d-inline-flex align-items-center fs-5">
						<a class="px-2" href="https://www.facebook.com/">
							<i class="fa-brands fa-facebook"></i>
						</a>
						<a class="px-2" href="https://twitter.com/">
							<i class="fa-brands fa-square-x-twitter"></i>
						</a>
						<a class="px-2" href="https://www.linkedin.com/">
							<i class="fa-brands fa-linkedin"></i>
						</a>
						<a class="px-2" href="https://www.instagram.com/">
							<i class="fa-brands fa-instagram"></i>
						</a>
						<a class="ps-2" href="https://www.youtube.com/">
							<i class="fa-brands fa-youtube"></i>
						</a>
					</div>
				</div>
				<div class="col-12 col-md-4 text-center text-md-end">
					<img src={paymentsImage} alt="Accepted Payments" />
				</div>
			</div>
		</div>
	</div>
</footer>
