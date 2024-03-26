<script>
	import 'tiny-slider/dist/tiny-slider.css';
	import { onMount } from 'svelte';
	import { tns } from 'tiny-slider';
	import PersonImgUrl from '$lib/assets/about/testimonials/person-1.png';

	onMount(() => {
		tns({
			container: '.testimonial-slider',
			items: 1,
			axis: 'horizontal',
			controlsContainer: '#testimonial-nav',
			swipeAngle: false,
			speed: 700,
			nav: true,
			controls: true,
			autoplay: true,
			autoplayTimeout: 3500,
			autoplayButtonOutput: false
		});
	});
</script>

<div class="pt-5 mt-5" style="padding-bottom: 7em;">
	<div class="container">
		<div class="row">
			<div class="col-lg-7 mx-auto text-center">
				<h2 class="section-title">Testimonials</h2>
			</div>
		</div>

		<div class="row justify-content-center align-items-center mt-5">
			<div class="col-lg-12">
				<div class="testimonial-slider-wrap text-center">
					<div id="testimonial-nav">
						<span class="prev" data-controls="prev"><span class="fa fa-chevron-left"></span></span>
						<span class="next" data-controls="next"><span class="fa fa-chevron-right"></span></span>
					</div>

					<div class="testimonial-slider">
						<div class="item">
							<div class="row justify-content-center">
								<div class="col-lg-8 mx-auto">
									<div class="testimonial-block text-center">
										<blockquote class="mb-5">
											<p>
												&ldquo;Donec facilisis quam ut purus rutrum lobortis. Donec vitae odio quis
												nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam vulputate velit
												imperdiet dolor tempor tristique. Pellentesque habitant morbi tristique
												senectus et netus et malesuada fames ac turpis egestas. Integer convallis
												volutpat dui quis scelerisque.&rdquo;
											</p>
										</blockquote>

										<div class="author-info">
											<div class="author-pic">
												<img src={PersonImgUrl} alt="Maria Jones" class="img-fluid" />
											</div>
											<h3 class="font-weight-bold">Maria Jones</h3>
											<span class="position d-block mb-3">CEO, Co-Founder, XYZ Inc.</span>
										</div>
									</div>
								</div>
							</div>
						</div>
						<!-- END item -->

						<div class="item">
							<div class="row justify-content-center">
								<div class="col-lg-8 mx-auto">
									<div class="testimonial-block text-center">
										<blockquote class="mb-5">
											<p>
												&ldquo;Donec facilisis quam ut purus rutrum lobortis. Donec vitae odio quis
												nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam vulputate velit
												imperdiet dolor tempor tristique. Pellentesque habitant morbi tristique
												senectus et netus et malesuada fames ac turpis egestas. Integer convallis
												volutpat dui quis scelerisque.&rdquo;
											</p>
										</blockquote>

										<div class="author-info">
											<div class="author-pic">
												<img src={PersonImgUrl} alt="Maria Jones" class="img-fluid" />
											</div>
											<h3 class="font-weight-bold">Maria Jones</h3>
											<span class="position d-block mb-3">CEO, Co-Founder, XYZ Inc.</span>
										</div>
									</div>
								</div>
							</div>
						</div>
						<!-- END item -->

						<div class="item">
							<div class="row justify-content-center">
								<div class="col-lg-8 mx-auto">
									<div class="testimonial-block text-center">
										<blockquote class="mb-5">
											<p>
												&ldquo;Donec facilisis quam ut purus rutrum lobortis. Donec vitae odio quis
												nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam vulputate velit
												imperdiet dolor tempor tristique. Pellentesque habitant morbi tristique
												senectus et netus et malesuada fames ac turpis egestas. Integer convallis
												volutpat dui quis scelerisque.&rdquo;
											</p>
										</blockquote>

										<div class="author-info">
											<div class="author-pic">
												<img src={PersonImgUrl} alt="Maria Jones" class="img-fluid" />
											</div>
											<h3 class="font-weight-bold">Maria Jones</h3>
											<span class="position d-block mb-3">CEO, Co-Founder, XYZ Inc.</span>
										</div>
									</div>
								</div>
							</div>
						</div>
						<!-- END item -->
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<style lang="scss">
	.testimonial-slider-wrap {
		position: relative;

		& .tns-inner {
			padding-top: 30px;
		}

		& .item {
			& .testimonial-block {
				& blockquote {
					font-size: 16px;

					@media (min-width: 768px) {
						line-height: 32px;
						font-size: 18px;
					}
				}

				& .author-info {
					& .author-pic {
						margin-bottom: 20px;
						& img {
							max-width: 80px;
							border-radius: 50%;
						}
					}
					& h3 {
						font-size: 14px;
						font-weight: 700;
						color: #2f2f2f;
						margin-bottom: 0;
					}
				}
			}
		}

		& .tns-nav {
			position: absolute;
			bottom: -50px;
			left: 50%;
			-webkit-transform: translateX(-50%);
			-ms-transform: translateX(-50%);
			transform: translateX(-50%);

			& button {
				background: none;
				border: none;
				display: inline-block;
				position: relative;
				width: 0 !important;
				height: 7px !important;
				margin: 2px;
			}

			& button:hover:before,
			button.tns-nav-active:before {
				background-color: #3b5d50;
			}

			& button:active,
			button:focus,
			button:hover {
				outline: none;
				-webkit-box-shadow: none;
				box-shadow: none;
				background: none;
			}

			& button:before {
				display: block;
				width: 7px;
				height: 7px;
				left: 0;
				top: 0;
				position: absolute;
				content: '';
				border-radius: 50%;
				-webkit-transition: 0.3s all ease;
				-o-transition: 0.3s all ease;
				transition: 0.3s all ease;
				background-color: #d6d6d6;
			}
		}

		& #testimonial-nav {
			position: absolute;
			top: 50%;
			z-index: 99;
			width: 100%;
			display: none;

			& > span {
				cursor: pointer;
				position: absolute;
				width: 58px;
				height: 58px;
				line-height: 58px;
				border-radius: 50%;
				background: rgba(59, 93, 80, 0.1);
				color: #2f2f2f;
				-webkit-transition: 0.3s all ease;
				-o-transition: 0.3s all ease;
				transition: 0.3s all ease;
			}

			& > span:hover {
				background: #3b5d50;
				color: #ffffff;
			}

			& .prev {
				left: -10px;
			}

			& .next {
				right: 0;
			}

			@media (min-width: 768px) {
				display: block;
			}
		}
	}

	:global(.tns-visually-hidden) {
		display: none;
	}
</style>
