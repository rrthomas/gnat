# Top-level Makefile.am

SUBDIRS = src

release: distcheck
	git diff --exit-code && \
	git tag -a -m "Release tag" "v$(VERSION)" && \
	git push && git push --tags && \
	woger github \
		github_user=rrthomas \
		package=pforth \
		version=$(VERSION) \
		dist_type=tar.gz