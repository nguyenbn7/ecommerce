<script>
	import WhyUsImgUrl from '$lib/assets/about/why-us/why-us.jpg';
</script>

<div class="py-5 why-choose-section">
	<div class="container">
		<div class="row justify-content-between align-items-center">
			<div class="col-lg-6">
				<h2 class="section-title">Why Choose Us</h2>
				<p>
					Donec vitae odio quis nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam vulputate
					velit imperdiet dolor tempor tristique.
				</p>

				<div class="row my-5">
					<div class="col-6 col-md-6">
						<div class="feature">
							<div class="icon">
								<i class="fa-solid fa-truck fs-3 pe-2 pb-2"></i>
							</div>
							<h3>Fast &amp; Free Shipping</h3>
							<p>
								Donec vitae odio quis nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam
								vulputate.
							</p>
						</div>
					</div>

					<div class="col-6 col-md-6">
						<div class="feature">
							<div class="icon">
								<i class="fa-solid fa-box fs-3 pe-2 pb-2"></i>
							</div>
							<h3>Easy to Shop</h3>
							<p>
								Donec vitae odio quis nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam
								vulputate.
							</p>
						</div>
					</div>

					<div class="col-6 col-md-6">
						<div class="feature">
							<div class="icon">
								<i class="fa-solid fa-arrows-rotate fs-3 pe-2 pb-2"></i>
							</div>
							<h3>24/7 Support</h3>
							<p>
								Donec vitae odio quis nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam
								vulputate.
							</p>
						</div>
					</div>

					<div class="col-6 col-md-6">
						<div class="feature">
							<div class="icon">
								<i class="fa-solid fa-arrow-right-arrow-left fs-3 pe-2 pb-2"></i>
							</div>
							<h3>Hassle Free Returns</h3>
							<p>
								Donec vitae odio quis nisl dapibus malesuada. Nullam ac aliquet velit. Aliquam
								vulputate.
							</p>
						</div>
					</div>
				</div>
			</div>

			<div class="col-lg-5">
				<div class="img-wrap">
					<img src={WhyUsImgUrl} alt="Why us" class="img-fluid" />
				</div>
			</div>
		</div>
	</div>
</div>

<style lang="scss">
	.why-choose-section {
		& .img-wrap {
			position: relative;
			&:before {
				position: absolute;
				content: '';
				width: 255px;
				height: 217px;
				background-image: url($lib/assets/about/why-us/dots-yellow.svg);
				background-repeat: no-repeat;
				background-size: contain;
				-webkit-transform: translate(-40%, -40%);
				-ms-transform: translate(-40%, -40%);
				transform: translate(-40%, -40%);
				z-index: -1;
			}

			& img {
				border-radius: 20px;
			}
		}
	}

	.feature {
		margin-bottom: 30px;

		& .icon {
			display: inline-block;
			position: relative;
			margin-bottom: 20px;

			&:before {
				content: '';
				width: 33px;
				height: 33px;
				position: absolute;
				background: rgba(59, 93, 80, 0.2);
				border-radius: 50%;
				right: -8px;
				bottom: 0;
			}
		}

		& h3 {
			font-size: 14px;
			color: #2f2f2f;
		}

		& .feature {
			& p {
				font-size: 14px;
				line-height: 22px;
				color: #6a6a6a;
			}
		}
	}
</style>
