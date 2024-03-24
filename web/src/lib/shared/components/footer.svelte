<script>
	import paymentsImage from '$lib/assets/images/payments.png';
	import { APP_NAME } from '../../core/constant';
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
								class="nav-link p-0 text-body-secondary"
							>
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
						<!-- TODO: move this to contact page -->
						<!-- <li class="nav-item mt-4 mb-3">
							<iframe
								class="w-100"
								src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3323.356430445871!2d-101.8624006172556!3d33.59605527293671!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x86fe12c4f4115643%3A0x4be2acd0830de582!2s199%20Avenue%20S%2C%20Lubbock%2C%20TX%2079415%2C%20Hoa%20K%E1%BB%B3!5e0!3m2!1svi!2s!4v1701750814877!5m2!1svi!2s"
								style="border:0; min-height: 300px;"
								loading="lazy"
								referrerpolicy="no-referrer-when-downgrade"
								title="199 Heffernan Avenue, Texas, 77218, United States"
							></iframe>
						</li> -->
					</ul>
				</div>
				<div class="col-md-8">
					<div class="row">
						<div class="col-3 col-md-2 mb-3">
							<h5>About {APP_NAME}</h5>
							<ul class="nav flex-column">
								<li class="nav-item mb-2">
									<a
										href="/about"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Story
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href="/shop"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Shop
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href="/blog"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Blog
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href="/careers"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Careers
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href="/checkout"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Checkout
									</a>
								</li>
							</ul>
						</div>

						<div class="col-9 col-md-4 ps-5 mb-3">
							<h5>Support</h5>
							<ul class="nav flex-column">
								<li class="nav-item mb-2">
									<a
										href="/contact"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Contact Us
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href="/faq"
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Shipping & Returns
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href={'#'}
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Tracking Order
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href={'#'}
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
										Frequently Asked Questions
									</a>
								</li>
								<li class="nav-item mb-2">
									<a
										href={'#'}
										class="link-underline-secondary link-underline-opacity-0 link-underline-opacity-75-hover p-0 text-secondary"
									>
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
										placeholder="Email address"
									/>
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
